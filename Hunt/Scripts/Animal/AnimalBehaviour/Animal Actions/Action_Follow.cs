
using UnityEngine;

namespace HT
{
    [CreateAssetMenu(fileName = "Action_Follow_player", menuName = "Games/Hunt/AI/AnimalActions/Movement/FollowPlayer")]
    public class Action_Follow : AnimalAction
    {
        public override void Awake() => base.Awake();

        public override void Execute(AnimalController animalController)
        {
            base.Execute(animalController);
            animalController.Execute_FollowPlayer_Command();
        }
    }
}
