using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

namespace HT
{
    public class UI_Buy : MonoBehaviour
    {
        private UI_SpawnCount uI_SpawnCount;
        [SerializeField] private NutrientPlacer placer;
        [SerializeField] private Image refillTime;
        [SerializeField] private bool isAvailable;
        [SerializeField] private Button useBtn;


        public void SetUp(UI_SpawnCount ui)
        {
            uI_SpawnCount = ui;

            Refill().Forget();

            useBtn.onClick.AddListener(() =>
            {
                if (isAvailable)
                {
                    placer.gameObject.SetActive(true);
                    placer.PlaceInScene(UpdateSpawnView, uI_SpawnCount.FollowMouse, CompleteSpawn);
                    Refill().Forget();
                }
            });
        }

        private async UniTaskVoid Refill()
        {
            float wait = 10;
            float count = 0;

            while (count <= wait)
            {
                count += Time.deltaTime;
                refillTime.fillAmount = count / wait;
                await UniTask.Yield();
                isAvailable = true;
            }

        }

        private void UpdateSpawnView(int count, int total) => uI_SpawnCount.SetData(count, total);
        private void CompleteSpawn() => uI_SpawnCount.gameObject.SetActive(false);

    }
}
