
using UnityEngine;

namespace HT
{
    [CreateAssetMenu(fileName = "Action_Tired", menuName = "Games/Hunt/AI/AnimalActions/Tired")]
    public class Action_Tired : AnimalAction
    {
        public override void Awake() => base.Awake();

        public override void Execute(AnimalController animalController)
        {
            base.Execute(animalController);
            animalController.Execute_Rest_Action(5);
        }
    }
}
