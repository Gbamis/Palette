
using UnityEngine;

namespace HT
{
     [CreateAssetMenu(fileName = "Action_Flee", menuName = "Games/Hunt/AI/AnimalActions/Flee")]
    public class Action_Flee : AnimalAction
    {
        public override void Awake() => base.Awake();

        public override void Execute(AnimalController animalController)
        {
            base.Execute(animalController);
            animalController.Execute_Flee_Action(1);
        }
    }
}
