using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using Zenject;

namespace HT
{
    public class VaccinePlacer : NutrientPlacer
    {
        private Action<int, int> _updateQuanity;
        private Action<Vector3> _followMouse;
        private Action _completed;
        private int spawnCount;

        public int totalCount;
        public Texture2D cursorIcon;
        public LayerMask layerMask;

        public override void PlaceInScene(
        Action<int, int> OnUpdateQuantity,
        Action<Vector3> OnFollowMouse,
        Action OnCompleted)
        {
            _updateQuanity = OnUpdateQuantity;
            _followMouse = OnFollowMouse;
            _completed = OnCompleted;

            spawnCount = totalCount;
            Routine.Instance.mousePointer.Set(cursorIcon);
            Move().Forget();
        }

        private async UniTaskVoid Move()
        {
            while (spawnCount > 0)
            {

                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hit, 200, layerMask))
                    {
                        if (hit.collider != null)
                        {
                            //vfx
                            //sfx
                            //place prefab

                            IHealth health = hit.collider.GetComponent<IHealth>();
                            health.Vaccinate();
                            spawnCount--;
                            _updateQuanity?.Invoke(spawnCount, totalCount);
                        }
                    }

                }

                await UniTask.Yield();
            }
            Routine.Instance.mousePointer.Default();
            _completed();
        }
    }
}
