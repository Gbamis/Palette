using UnityEngine;

namespace HT
{
    public enum HAND { LEFT, RIGHT };

    public abstract class BaseWeapon
    {
        public static int i_attack = Animator.StringToHash("attack");
        public static int i_swap = Animator.StringToHash("swapped");

        protected GameObject spawnedWeapon;
        protected float _next_allowed;
        protected float _useRate;
        protected Animator _animator;

        public float field_of_view;
        public float view_distance;
        public float damage_amount;
        public string _subStateMachine;
        public bool _canSwap;
        
        
        public virtual void ActivateWeapon(WeaponConfig weaponConfig, Transform handle, Animator anim, float damage = 0)
        {
            field_of_view = weaponConfig.field_of_view;
            view_distance = weaponConfig.view_distance;
            damage_amount = weaponConfig.damage_amount;
            _next_allowed = 0;

            _animator = anim;
            _subStateMachine = weaponConfig.subStateMachine;
            _canSwap = weaponConfig.swapAnimPlaces;
            _useRate = weaponConfig.rate_of_use;
            _animator.SetBool(_subStateMachine, true);

            if (!spawnedWeapon)
            {
                spawnedWeapon = MonoBehaviour.Instantiate(weaponConfig.weaponPrefab, handle);
                spawnedWeapon.transform.position = handle.position;
                spawnedWeapon.transform.localEulerAngles = weaponConfig.spawnRotation;
               // spawnedWeapon.transform.SetPositionAndRotation(handle.position, Quaternion.Euler(weaponConfig.spawnRotation));
                float scale = weaponConfig.spawnScale;
                spawnedWeapon.transform.localScale = new Vector3(scale, scale, scale);
            }
            else
            {
                spawnedWeapon.SetActive(true);
            }

        }

        public abstract void DeactivateWeapon();

        public abstract void UseWeaponOn(Transform obj);
    }

}