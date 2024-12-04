using UnityEngine;

namespace HT
{
    [CreateAssetMenu(fileName = "CombatAbility", menuName = "Games/Hunt/Abilities/Combat")]
    public class Ability_Combat : Ability
    {
        [SerializeField] private IWeapon currentWeapon;

        public override void OnInit(PlayerConfig playerConfig)
        {
            base.OnInit(playerConfig);
            config.player_input.Gameplay.Combat.performed += (ctx) =>
            {
                if (currentWeapon != null)
                {
                    currentWeapon.Use(playerConfig.controller.transform);
                }
            };
        }

        public override void OnReset() => currentWeapon.DeactivateWeapon();

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        public void SwitchWeapon(IWeapon weapon, Transform goal, Animator anim)
        {
            if (currentWeapon != null) { currentWeapon.DeactivateWeapon(); }
            currentWeapon = weapon;
            //currentWeapon.ActivateWeapon(goal, anim);
        }

        public void DisbaleWeapons() => currentWeapon.DeactivateWeapon();
    }
}
