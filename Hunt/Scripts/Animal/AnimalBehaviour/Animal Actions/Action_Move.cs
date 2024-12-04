using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Unity.Android.Gradle.Manifest;

namespace HT
{
    [CreateAssetMenu(fileName = "Action_Move", menuName = "Games/Hunt/AI/AnimalActions/Move")]
    public class Action_Move : AnimalAction
    {
        private bool active;
        public LayerMask mask;
        public Texture2D cursor;

        public override void Awake() => base.Awake();

        public override void Execute(AnimalController animalController)
        {
            base.Execute(animalController);

            active = true;
            Routine.Instance.moveIcon.gameObject.SetActive(true);
            OnUpdate(animalController).Forget();
            Routine.Instance.mousePointer.Set(cursor);
        }

        private async UniTask OnUpdate(AnimalController animalController)
        {
            while (active)
            {
                OnMouseMove(animalController);
                await UniTask.Yield();
            }
        }

        private void OnMouseMove(AnimalController animalController)
        {
            /*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 200, mask))
            {
                if (hit.collider != null)
                {
                    Routine.Instance.moveIcon.transform.position = hit.point;
                    if (Input.GetMouseButton(0))
                    {
                        active = false;
                        animalController.Execute__Move_Command(Routine.Instance.moveIcon.transform);
                        Routine.Instance.moveIcon.Dropped();
                        Routine.Instance.mousePointer.Default();
                    }
                }
                else
                {
                    Routine.Instance.moveIcon.Dropped();
                    Routine.Instance.mousePointer.Default();
                }
            }*/
        }
    }

}