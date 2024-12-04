
using UnityEngine;

namespace HT
{
    [CreateAssetMenu(fileName = "Action_Sleep", menuName = "Games/Hunt/AI/AnimalActions/Sleep")]
    public class Action_Sleep : AnimalAction
    {
        public override void Awake() => base.Awake();

        public override void Execute(AnimalController animalController)
        {
            base.Execute(animalController);
            animalController.Execute_Rest_Action(5);
        }
    }
}
