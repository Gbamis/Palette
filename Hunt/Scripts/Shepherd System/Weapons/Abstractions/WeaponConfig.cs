using UnityEngine;

namespace HT
{
    [CreateAssetMenu(fileName = "weapon", menuName = "Games/Hunt/Shepherd/Weapon/WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {
        [Header("Prefab")]
        public GameObject weaponPrefab;
        public Vector3 spawnRotation;
        public float spawnScale;


        [Header("Animation")]
        public string subStateMachine;
        public HAND spawnAtGoal;
        public bool swapAnimPlaces;

        [Header("Handling")]
        public float rate_of_use;
        public float damage_amount;
        public float field_of_view;
        public float view_distance;

    }
}
