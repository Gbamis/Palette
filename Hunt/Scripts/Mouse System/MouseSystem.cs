using UnityEngine;
using Unity.Cinemachine;
using Zenject;

namespace HT
{
    public class MouseSystem : MonoBehaviour
    {
        [Inject] private readonly GameplayEvent gameplayEvent;
        [Inject] private readonly MouseMode mouseMode;

        public Transform gameCam;

        public CinemachineInputAxisController gameplayController;
        public CinemachineCamera orthoCam;
        public CinemachineOrbitalFollow orbitComp;

        private void Start()
        {
            gameCam.gameObject.SetActive(true);
            mouseMode.mode = MouseMode.Mode.CAMERA;
        }


        private void Update()
        {
            if (Input.GetMouseButton(0) && mouseMode.mode == MouseMode.Mode.CAMERA)
            {
                Activate();
            }
            else
            {
                DeActivate();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //gameEvent.OnGet_CommandSystem().RemoveAllSelected();
            }

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 200))
                {
                    if (hit.collider != null)
                    {
                        hit.collider.TryGetComponent(out Animal animal);
                        if (animal == null)
                        {
                            gameplayEvent.OnUnit_Move?.Invoke();
                        }
                    }
                }
            }

            Zoom();
        }


        private void Activate() => gameplayController.enabled = true;

        private void DeActivate() => gameplayController.enabled = false;

        private void Zoom()
        {

            if (Input.mouseScrollDelta.y != 0)
            {

                if (!Camera.main.orthographic)
                {
                    float lastRadius = orbitComp.Radius;
                    lastRadius += Input.mouseScrollDelta.y * -1;
                    lastRadius = Mathf.Clamp(lastRadius,14, 17);
                    orbitComp.Radius = lastRadius;
                }
                else
                {
                    float lastValue = orthoCam.Lens.OrthographicSize;

                    lastValue += Input.mouseScrollDelta.y * -1;
                    lastValue = Mathf.Clamp(lastValue, 5, 7);
                    orthoCam.Lens.OrthographicSize = lastValue;
                }
            }

        }

    }

}