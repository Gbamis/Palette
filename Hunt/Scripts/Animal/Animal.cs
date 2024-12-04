using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HT
{
    public enum Gender { MALE, FEMALE, NONE }

    public class Animal : MonoBehaviour, IGoat, IHoverable, IHealth
    {
        [SerializeField] private GameplayEvent gameplayEvent;
        [SerializeField] private UIEvents uiEvent;

        private GameObject virusIndiccator;
        private bool isSick;
        private GoatInfoModel goatInfoModel;

        public Gene gene { set; get; }
        public Gender gender { set; get; }

        public MouseMode mouseMode;
        public AnimalBehaviour animalBehaviour;
        public AnimalController animalController;
        public AnimalPhysical animalPhysical;
        public ParticleSystem spawnFX;
        public string Genenome;
        public AnimalStat animalStat;

        public void Birth(Gene m_gene, Gender m_gender, bool canControl = false)
        {
            gene = m_gene;
            gender = m_gender;
            animalStat = new AnimalStat(transform, gene, m_gender)
            {
                CanControl = canControl
            };

            animalBehaviour.Init(animalStat);
            animalController.Init(animalStat, gene);
            animalPhysical.SetPhysicalCharcateristics(gene.color);

            Genenome = m_gene.StringRepresentation();
            spawnFX.gameObject.SetActive(true);
        }

        public void Death()
        {
            SFX.Core.Play_GoatDestroyed();
            GameObject fx = gameplayEvent.OnGetObjectFromPool?.Invoke();
            fx.SetActive(true);
            fx.transform.position = transform.position;
            Destroy(gameObject);

        }

        public void Buy() => gameplayEvent.OnAnimalBought(this, animalStat.gender);
        public void Vaccinate() { animalStat.ResetStamina(); isSick = false; }
        public bool isOnHeat() => animalStat.GetHeatLevel() > 0.4F;

        public void TriggerSickness(float rate)
        {
            isSick = true;
            virusIndiccator = virusIndiccator != null ? virusIndiccator : Instantiate(gameplayEvent.virusPrefab, transform.position, Quaternion.identity, transform);
            virusIndiccator.SetActive(true);

            StartCoroutine(KillHealth());

            IEnumerator KillHealth()
            {
                while (isSick && animalStat.GetStamina() > 0)
                {
                    virusIndiccator.transform.GetChild(0).transform.LookAt(Camera.main.transform);
                    animalStat.ReduceStamina(gene.tirednessRate * 20 * gene.diseaseRate);

                    yield return null;
                }
                virusIndiccator.SetActive(false);
            }
        }


        public void OnPointerEnter(PointerEventData ped) => OnHighlight();
        public void OnPointerExit(PointerEventData ped) => OnDeHightlight();
        public void OnHighlight()
        {
            if (mouseMode.mode != MouseMode.Mode.NUTRITION)
            {
                mouseMode.mode = MouseMode.Mode.UNIT;
            }

            goatInfoModel.SetModel(animalStat.gender, animalStat.GetStamina(), animalStat.GetHeatLevel(), transform.position);
            uiEvent.OnShowGoatInfo(goatInfoModel);

            if (animalStat.gender == Gender.FEMALE)
            {
                gameplayEvent.OnUnit_Hover?.Invoke(transform.position);
            }
        }
        public void OnDeHightlight()
        {
            uiEvent.OnHideGoatInfo();
            if (mouseMode.mode != MouseMode.Mode.NUTRITION)
            {
                mouseMode.mode = MouseMode.Mode.CAMERA;
            }
            gameplayEvent.OnUnit_UnHover?.Invoke();
        }


    }

}