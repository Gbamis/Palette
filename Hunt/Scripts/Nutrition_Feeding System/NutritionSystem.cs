using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace HT
{
    public class NutritionSystem : MonoBehaviour
    {
        [Inject] private readonly AppEvent appEvent;
        [SerializeField] UI_NutritionStore_View ui_view;

        private void Start() => appEvent.OnGameStarted += () => ui_view.StoreRefilled();
    }
}
