using UnityEngine;

namespace HT
{
    //[CreateAssetMenu(fileName = "MoveAbility", menuName = "Games/Hunt/Controls/Move")]
    public class Control : ScriptableObject
    {
        public virtual void OnUpdate(Config config){}
    }

}