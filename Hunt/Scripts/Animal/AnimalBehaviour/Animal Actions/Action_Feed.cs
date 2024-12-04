
using UnityEngine;

namespace HT
{
    [CreateAssetMenu(fileName = "Action_Feed", menuName = "Games/Hunt/AI/AnimalActions/Feed")]
    public class Action_Feed : AnimalAction
    {
        public override void Awake() => base.Awake();

        public override void Execute(AnimalController animalController)
        {
            base.Execute(animalController);
            animalController.Execute_Feed_Action(4);
        }
    }
}
