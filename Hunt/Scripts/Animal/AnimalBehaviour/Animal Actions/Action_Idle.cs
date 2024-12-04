
using UnityEngine;

namespace HT
{
    [CreateAssetMenu(fileName = "Action_Idle", menuName = "Games/Hunt/AI/AnimalActions/Idle")]
    public class Action_Idle : AnimalAction
    {
        private int randIdle;
        public override void Awake()
        {
            base.Awake();
            randIdle = 0;
        }

        public override void Execute(AnimalController animalController)
        {
            base.Execute(animalController);
            //Random.InitState(System.DateTime.Now.Millisecond);
            //int rand = Random.Range(2,4);
            animalController.Execute_Idle_Action(3);
        }

    }
}
