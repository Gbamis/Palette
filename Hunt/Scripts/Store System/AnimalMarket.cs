using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace HT
{
    [System.Serializable]
    public struct StoreItem
    {
        public AnimalGene gene;
        public int price;
    }

    public class AnimalMarket : MonoBehaviour
    {
        [Inject] private AppEvent appEvent;
        [Inject] private GameplayEvent gameplayEvent;
        [Inject] private UIEvents uiEvent;

        private Dictionary<Animal, int> availableAnimals;
        public BankAccount bankAccount;
        public Transform storeArea;
        public List<AnimalAction> actionAppend;
        public List<StoreItem> storeItems;


        [Header("UI")]
        [SerializeField] private Sprite markerIcon;
        private UI_ScreenMarker ui_marker;


        public void Start()
        {
            gameplayEvent.OnAnimalBought += (anim, gen) => AnimalBought(anim, gen);
            availableAnimals = new Dictionary<Animal, int>();
            appEvent.OnGameStarted += () => AddNewStock();
        }

        public void AddNewStock()
        {
            try { StartCoroutine(Add()); }
            catch (System.Exception e) { }

            IEnumerator Add()
            {
                yield return new WaitForSeconds(1);
                ui_marker = uiEvent.OnCreateMarker(storeArea, markerIcon);

                foreach (StoreItem st in storeItems)
                {
                    Animal animal = gameplayEvent.OnCreateAnimalFromGene(st.gene, true);
                    availableAnimals.Add(animal, st.price);
                    animal.transform.SetParent(storeArea);

                    Vector3 pos = Random.insideUnitCircle * 4;
                    pos.x += storeArea.position.x;
                    pos.z += storeArea.position.z;
                    pos.y = storeArea.position.y;
                    animal.transform.position = pos;
                }
            }

        }

        public int GetAnimalPrice(Animal animal) => availableAnimals[animal];

        private void AnimalBought(Animal animal, Gender gender)
        {
            int amount = GetAnimalPrice(animal);
            bankAccount.Debit(amount);
            animal.animalStat.CanControl = true;

            foreach (AnimalAction ac in actionAppend)
            {
                animal.animalBehaviour.AddAnimalAction(ac);
            }
            //gameEvent.OnGet_BreedingSystem().AddNumber(gender);

            ui_marker.gameObject.SetActive(false);
        }
    }

}