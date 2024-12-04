using UnityEngine;

namespace HT
{
    [CreateAssetMenu(fileName = "ShepherdData", menuName = "Games/Hunt/Shepherd/Data")]
    public class ShepherdData : ScriptableObject
    {
        public Shepherd shepherdPrefab;
        public WeaponFactory weaponFactory;
        public Sprite icon;
        public LayerMask layerMask;
        public int attainedLevel;
        public int dataHash;

    }
}
