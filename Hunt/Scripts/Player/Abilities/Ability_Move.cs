using UnityEngine;

namespace HT
{
    [CreateAssetMenu(fileName = "MoveAbility", menuName = "Games/Hunt/Abilities/Move")]
    public class Ability_Move : Ability
    {
        private Vector2 move;
        private Vector3 dir;

        private Vector3 fw;
        private Vector3 rt;

        public float lookSpeed;

        public float mag;

        public override void OnInit(PlayerConfig playerConfig)
        {
            base.OnInit(playerConfig);

            config.player_input.Gameplay.Move.performed += (ctx) => move = ctx.ReadValue<Vector2>();
            config.player_input.Gameplay.Move.canceled += ctx => move = Vector2.zero;
        }

        public override void OnReset()
        {
            move = Vector2.zero;
            config.anim.SetFloat("forward",0);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            fw = config.cam.forward;
            rt = config.cam.right;

            Move();
            Direct();
        }


        private void Move()
        {
            float x = Mathf.Abs(move.x);
            float y = Mathf.Abs(move.y);

            mag = move.sqrMagnitude;
            config.anim.SetFloat("forward",mag);
            
            fw.y = 0;
            rt.y = 0;
            dir = (fw * move.y) + (rt * move.x);
            //config.controller.Move(dir * Time.deltaTime);

        }
        public void Direct()
        {
            fw.y = 0;
            rt.y = 0;
            fw.Normalize();
            rt.Normalize();
            var dis = fw * move.y + rt * move.x;
            Vector3 direction = Vector3.RotateTowards(config.controller.transform.forward, dis, lookSpeed * Time.deltaTime, 0.0f);
            config.controller.transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
