using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using Zenject;

namespace HT
{
    public class GrassSpawner : NutrientPlacer
    {

        [SerializeField] private UI_SpawnCount uI_SpawnCount;
        [SerializeField] private Transform root;
        private Action<int, int> _updateQuanity;
        private Action<Vector3> _followMouse;
        private Action _completed;
        private int spawnCount;
        private GameObject clone;

        public GameObject prefab;
        public int totalCount;
        public Texture2D cursorIcon;
        public LayerMask layerMask;

        public AudioSource click;

        public override void PlaceInScene(
        Action<int, int> OnUpdateQuantity,
        Action<Vector3> OnFollowMouse,
        Action OnCompleted)
        {
            _updateQuanity = OnUpdateQuantity;
            _followMouse = OnFollowMouse;
            _completed = OnCompleted;

            spawnCount = totalCount;
            Spawn();
            Routine.Instance.mousePointer.Set(cursorIcon);
            Move().Forget();
        }

        private void Spawn()
        {
            if (spawnCount > 0)
            {
                clone = Instantiate(prefab, root);
                clone.SetActive(true);

                float rot = UnityEngine.Random.Range(0, 360);
                clone.transform.Rotate(Vector3.up * rot);
            }
        }

        private async UniTaskVoid Move()
        {
            mouseMode.mode = MouseMode.Mode.NUTRITION;
            while (spawnCount > 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 200, layerMask))
                {
                    if (hit.collider != null)
                    {
                        clone.transform.position = hit.point;

                        _followMouse?.Invoke(hit.point);

                        if (Input.GetMouseButtonDown(0))
                        {
                            //vfx
                            //sfx
                            //place prefab
                            Spawn();
                            spawnCount--;

                            _updateQuanity?.Invoke(spawnCount, totalCount);
                            GameObject fx = gameplayEvent.OnGetObjectFromPool?.Invoke();
                            fx.SetActive(true);
                            fx.transform.position = hit.point;


                            click.Play();
                        }
                    }
                }
                await UniTask.Yield();
            }
            Routine.Instance.mousePointer.Default();
            _completed?.Invoke();
            mouseMode.mode = MouseMode.Mode.CAMERA;
        }
    }
}
