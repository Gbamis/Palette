using UnityEngine;

namespace HT
{
    [CreateAssetMenu(fileName = "Hammer", menuName = "Games/Hunt/Shepherd/Weapon/Factory/Hammer")]
    public class Factory_Hammer : WeaponFactory
    {
        public override BaseWeapon CreateWeapon()=>new Weapon_Hammer();
    }

    public class Weapon_Hammer : BaseWeapon
    {
        public override void DeactivateWeapon()
        {
            _animator.SetBool(_subStateMachine, false);
            spawnedWeapon.SetActive(false);
        }

        public override void UseWeaponOn(Transform obj)
        {
            if (Time.time < _next_allowed) { return; }
            if (spawnedWeapon != null)
            {
                _animator.SetTrigger(i_attack);
                if (_canSwap)
                {
                    bool swap = _animator.GetBool(i_swap);
                    swap = !swap;
                    _animator.SetBool(i_swap, swap);
                }

                _next_allowed = Time.time + _useRate;
            }
        }
    }
}
