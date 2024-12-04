using UnityEngine;

namespace HT
{
    [CreateAssetMenu(fileName = "MoveControl", menuName = "Games/Hunt/Control/Move")]
    public class MoveControl : Control
    {
        private Vector3 fw;
        private Vector3 rt;

        public MouseMode mouseMode;
        public float wasd_speed;

        public LayerMask clickMask;
        public bool canPlayerMove;

        public override void OnUpdate(Config config)
        {
            fw = config.cam.forward;
            rt = config.cam.right;

            if (canPlayerMove)
            {
                ClickMove(config.self);
                //WASD_Movement(config.self);
            }

        }

        private void ClickMove(Transform self)
        {
            if (Input.GetMouseButtonDown(1) && mouseMode.mode == MouseMode.Mode.CAMERA)
            {
                SFX.Core.ButtonClicked();
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100, clickMask))
                {
                    if (hit.collider != null)
                    {
                        self.position = hit.point;
                    }
                }
            }

        }

        private void WASD_Movement(Transform self)
        {
            if (mouseMode.mode == MouseMode.Mode.CAMERA)
            {
                float vertical = Input.GetAxis("Vertical");
                float horizontal = Input.GetAxis("Horizontal");

                Vector3 translation = Time.fixedDeltaTime * wasd_speed * new Vector3(horizontal, 0, vertical);

                self.Translate(translation);
            }

        }
    }

}