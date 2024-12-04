using UnityEngine;
using Cysharp.Threading.Tasks;

namespace HT
{
    [CreateAssetMenu(fileName = "Action_Breed", menuName = "Games/Hunt/AI/AnimalActions/Breeding/Mate")]
    public class Action_Breed : AnimalAction
    {
        public GlobalData globalData;
        public LayerMask mateMask;


        public override void Awake() => base.Awake();

        public override void Execute(AnimalController animalController)
        {
            base.Execute(animalController);
            Breed(animalController);
        }

        private void Breed(AnimalController animalController)
        {
            IGoat goat = animalController.GetComponent<IGoat>();
            if (goat.gender == Gender.MALE)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 200, mateMask))
                {
                    if (hit.collider != null)
                    {
                        IGoat other_goat = hit.collider.GetComponent<IGoat>();

                        Vector3 player = Routine.Instance.player.position;
                        float dist = Vector3.Distance(hit.collider.transform.position, player);

                        if (dist > globalData.selectionDistance)
                        {
                            animalController.gameplayEvent.OnShowSelectionRange?.Invoke();
                            return;
                        }
                        if (other_goat.gender == Gender.FEMALE && other_goat.isOnHeat())
                        {
                            float rate = animalController.GetComponent<Animal>().animalStat.gene.reproductionRate;
                            hit.collider.GetComponent<AnimalController>().
                            Execute_Reproduce_Action(goat, other_goat, rate);

                            Routine.Instance.mousePointer.Default();
                        }
                    }
                    else
                    {
                        Routine.Instance.mousePointer.Default();
                    }
                }
            }
            else
            {
                Routine.Instance.mousePointer.Default();
            }

        }

    }

}