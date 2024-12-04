using UnityEngine;
using System;
using UnityEngine.UI;

namespace HT
{
    [CreateAssetMenu(fileName = " UIEvents", menuName = "Games/Hunt/EventChannel/UIEvents")]
    public class UIEvents : ScriptableObject
    {
        public Func<Transform, Sprite, UI_ScreenMarker> OnCreateMarker;
        public Action<GoatInfoModel> OnShowGoatInfo;
        public Action OnHideGoatInfo;
        public Action<Animal> OnShowAnimalMarketOptions;
    }
}
