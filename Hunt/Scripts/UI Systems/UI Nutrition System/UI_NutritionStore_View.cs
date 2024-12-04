using UnityEngine;
using System.Collections.Generic;

namespace HT
{
    public class UI_NutritionStore_View : UI_Views
    {
        public GameObject content;
        [SerializeField] private UI_SpawnCount ui_spawnCount;
        public List<UI_Buy> buys;

        private void Awake()
        {
            content.SetActive(false);
            ui_spawnCount.gameObject.SetActive(false);
        }
        public void StoreRefilled()
        {
            foreach (UI_Buy buy in buys)
            {
                buy.SetUp(ui_spawnCount);
            }
            content.SetActive(true);
        }
    }
}
