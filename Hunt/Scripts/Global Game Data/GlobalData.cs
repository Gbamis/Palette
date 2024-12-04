using UnityEngine;

namespace HT
{
    [CreateAssetMenu(fileName = " GlobalData", menuName = "Games/Hunt/GameConfig/GlobalData")]
    public class GlobalData : ScriptableObject
    {
        [Header("Player Seetings")]
        public float selectionDistance; //10.f
        public float lineRendererHieght;

        [Header("Breeding")]
        public int maxBreed;
        public float grassConsumptionAmount = 0.007f;
    }

}