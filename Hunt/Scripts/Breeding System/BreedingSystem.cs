using UnityEngine;
using System.Collections.Generic;
using Zenject;

namespace HT
{
    public class BreedingSystem : MonoBehaviour
    {
        [Inject] private readonly GameplayEvent gameplayEvent;
        [Inject] private readonly GlobalData globalData;

        [SerializeField] private UI_BreedMaxout_View uI_BreedMaxout_View;
        [SerializeField] private GameObject maxedView;

        private Animal male_goat;
        private Animal female_goat;
        private int maleCount = 0;
        private int femaleCount = 0;
        private int genderPick = 0;
        private List<int> openIndex;

        public Animal goat_prefab;
        public Transform spanwedCattle;
        public Transform breedLocation;
        public List<AnimalGene> animalGenes;


        private void OnEnable()
        {
            gameplayEvent.OnAnimalBought += (anim, gen) => AddNumber(gen);
            gameplayEvent.OnCreateAnimalFromGene += CreateAnimalFromGene;
            gameplayEvent.OnBreedGoat += (M, F, T) => BreedGoat(M, F, T);
            gameplayEvent.OnCheckIsBreedMaxed += () =>
            {
                bool maxed = (maleCount + femaleCount) > globalData.maxBreed;
                if (maxed)
                {
                    maxedView.SetActive(true);
                    Invoke(nameof(HideMaxed), 2);
                }
                return maxed;
            };
            gameplayEvent.OnGoatKilled += (ctx) => RemoveGoat(ctx);
        }

        private void OnDisable()
        {
            gameplayEvent.OnAnimalBought -= (anim, gen) => AddNumber(gen);
            gameplayEvent.OnCreateAnimalFromGene -= CreateAnimalFromGene;
            gameplayEvent.OnBreedGoat -= (M, F, T) => BreedGoat(M, F, T);
            gameplayEvent.OnCheckIsBreedMaxed -= () => (maleCount + femaleCount) > globalData.maxBreed; ;
        }

        private void Start()
        {
            openIndex = new() { 0, 1 };
            maxedView.SetActive(false);
        }

        private void HideMaxed() => maxedView.SetActive(false);

        private Animal CreateAnimal()
        {
            Animal animal = Instantiate(goat_prefab);
            animal.gameObject.SetActive(true);
            animal.transform.SetParent(spanwedCattle);
            return animal;
        }
        private Animal CreateAnimalWithTransform(Vector3 pos, Quaternion rot)
        {
            Animal animal = Instantiate(goat_prefab, pos, rot, spanwedCattle);
            animal.gameObject.SetActive(true);
            return animal;
        }
        public Animal CreateAnimalFromGene(AnimalGene animalGene, bool emptyActions = false)
        {
            Gender gender = Random.Range(0, 10) % 2 == 0 ? Gender.MALE : Gender.FEMALE;
            Animal offspring = CreateAnimal();
            Gene gene = CreateGeneFromAnimalData(animalGene);
            offspring.Birth(gene, gender);
            return offspring;
        }


        private Gene CreateGeneFromAnimalData(AnimalGene animalGene)
        {
            Gene gene = new()
            {
                color = animalGene.skinColor,
                maxGrowthSize = animalGene.maxGrowthSize,
                tirednessRate = animalGene.tirednessRate,
                diseaseRate = animalGene.diseaseRate,
                reproductionRate = animalGene.reproductionRate,
                red = animalGene.red,
                green = animalGene.green,
                blue = animalGene.blue
            };
            return gene;
        }
        private Gene CreateGeneFromParents(Gene father, Gene mother, Gender gender)
        {
            Gene gene = new()
            {
                color = MixSkinColor(father.color, mother.color),
                tirednessRate = MixTraits(father.tirednessRate, mother.tirednessRate),
                reproductionRate = gender == Gender.FEMALE ? MixTraits(father.reproductionRate, mother.reproductionRate) : mother.reproductionRate,
                diseaseRate = MixTraits(father.diseaseRate, mother.diseaseRate),
                red = MixTraits(father.red, mother.red),
                green = MixTraits(father.green, mother.green),
                blue = MixTraits(father.blue, mother.blue)
            };
            return gene;
        }

        private Color MixSkinColor(Color father, Color mother)
        {
            Color color;
            color.r = (father.r + mother.r) * .5f;
            color.g = (father.g + mother.g) * .5f;
            color.b = (father.b + mother.b) * .5f;
            color.a = 1;
            return color;
        }
        private float MixTraits(float male, float female) => (male + female) * .5f;

        public void BreedGoat(IGoat male, IGoat female, Transform trans)
        {
            Gender gender = genderPick == 0 ? Gender.MALE : Gender.FEMALE;
            genderPick++;
            if (genderPick > 1) { genderPick = 0; }
            
            Animal offspring = CreateAnimal();
            offspring.Birth(CreateGeneFromParents(male.gene, female.gene, gender), gender, true);
            Vector3 pos = GetRandomPositionFrom(trans.position, 4);
            offspring.gameObject.transform.position = pos;

            gameplayEvent.OnGoatProduced?.Invoke(offspring.gene.color, pos);

            AddNumber(gender);
        }
        private void BreedGoatFromAnimalGeneData(AnimalGene male, AnimalGene female)
        {
            Vector3 pos = GetRandomPositionFrom(breedLocation.position, 4);
            Quaternion rot = Quaternion.Euler(0, Random.Range(20, 270), 0);
            male_goat = male_goat != null ? male_goat : male_goat = CreateAnimalWithTransform(pos, rot);

            pos = GetRandomPositionFrom(breedLocation.position, 4);
            rot = Quaternion.Euler(0, Random.Range(20, 270), 0);
            female_goat = female_goat != null ? female_goat : female_goat = CreateAnimalWithTransform(pos, rot);

            Gene m_gene = CreateGeneFromAnimalData(male);
            Gene f_gene = CreateGeneFromAnimalData(female);

            male_goat.Birth(m_gene, Gender.MALE, true);
            female_goat.Birth(f_gene, Gender.FEMALE, true);
            maleCount = femaleCount = 1;

            gameplayEvent.OnBreedStatChanged?.Invoke(maleCount, femaleCount);
        }

        public void RemoveGoat(Animal animal)
        {
            RemoveNumber(animal.animalStat.gender);
            animal.Death();
        }

        public void GenerateRandomFirstGeneration()
        {

            int male_rand = GetRandomIndex();
            openIndex.Remove(male_rand);

            int female_rand = GetRandomIndex();
            openIndex.Remove(female_rand);

            BreedGoatFromAnimalGeneData(animalGenes[male_rand], animalGenes[female_rand]);
        }

        public Vector3 GetRandomPositionFrom(Vector3 other, float span = 1)
        {
            Vector2 rand = Random.insideUnitCircle * span;
            Vector3 pos = other;
            pos.x += rand.x;
            pos.z += rand.y;
            return pos;
        }

        public void AddNumber(Gender gender)
        {
            if (gender == Gender.MALE) { maleCount++; }
            else { femaleCount++; }

            uI_BreedMaxout_View.SetLevel(maleCount + femaleCount, globalData.maxBreed);
            gameplayEvent.OnBreedStatChanged?.Invoke(maleCount, femaleCount);
        }

        public void RemoveNumber(Gender gender)
        {
            if (gender == Gender.MALE) { maleCount--; }
            else { femaleCount--; }

            maleCount = maleCount < 0 ? 0 : maleCount;
            femaleCount = femaleCount < 0 ? 0 : femaleCount;

            uI_BreedMaxout_View.SetLevel(maleCount + femaleCount, globalData.maxBreed);
            gameplayEvent.OnBreedStatChanged?.Invoke(maleCount, femaleCount);
        }

        private int GetRandomIndex()
        {
            int index = Random.Range(0, openIndex.Count);
            return openIndex[index];
        }

    }

}