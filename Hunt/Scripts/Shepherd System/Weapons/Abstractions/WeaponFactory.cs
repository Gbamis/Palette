using UnityEngine;

namespace HT
{
    public class WeaponFactory : ScriptableObject
    {
        
        public BaseWeapon baseWeapon;
        public WeaponConfig weaponConfig;

        public virtual BaseWeapon CreateWeapon() => baseWeapon;

        public BaseWeapon ActivateWeaponFromUpgrade(Transform goal, Animator anim)
        {
            BaseWeapon weapon = CreateWeapon();
            weapon.ActivateWeapon(weaponConfig, goal, anim);
            return weapon;
        }

        public HAND Goal() => weaponConfig.spawnAtGoal;
    }

}