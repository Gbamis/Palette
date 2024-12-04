using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace HT
{
    [System.Serializable]
    public class PlayerConfig
    {
        [HideInInspector] public Animator anim;
        [HideInInspector] public CharacterController controller;
        [HideInInspector] public PlayerInput player_input;
        public Transform cam;
        public GameObject rangeIcon;
        public Transform rightGoal;
    }

    public class Player : MonoBehaviour
    {
        [Inject] private GameplayEvent gameplayEvent;
        private bool canUpdateAbilities;

        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private List<Ability> abilities;
        [SerializeField] private Ability_Combat combatAbility;


        private void Awake()
        {
            playerConfig.player_input = new PlayerInput();

            playerConfig.anim = GetComponent<Animator>();
            playerConfig.controller = GetComponent<CharacterController>();

           /* canUpdateAbilities = true;
            gameEvent.OnBlockPlayerInput += (ctx) =>
            {
                canUpdateAbilities = !ctx;
                if (ctx == true)
                {
                    foreach (Ability ab in abilities) { ab.OnReset(); }
                }
            };*/

            gameplayEvent.OnShowSelectionRange += ShowSelectionRange;

            //gameEvent.OnActivateCombatCanvas += (ctx) => { if (!ctx) { combatAbility.OnReset(); } };
            //gameEvent.OnWeaponEquipped += (ctx) => CombatAbility(ctx);

        }

        private void OnEnable() => playerConfig.player_input.Gameplay.Enable();
        private void OnDisable() => playerConfig.player_input.Gameplay.Disable();

        private void Start()
        {
            foreach (Ability ab in abilities) { ab.OnInit(playerConfig); }
            Routine.Instance.player = transform;
        }
        private void Update()
        {
            if (canUpdateAbilities) { foreach (Ability ab in abilities) { ab.OnUpdate(); } }
        }

        private void ShowSelectionRange()
        {
            playerConfig.rangeIcon.SetActive(true);
            Invoke(nameof(HideRnage), 2);
        }
        private void HideRnage() => playerConfig.rangeIcon.SetActive(false);

        public void CombatAbility(IWeapon weapon)
        {
            combatAbility.SwitchWeapon(weapon, playerConfig.rightGoal, playerConfig.anim);
        }


    }

}