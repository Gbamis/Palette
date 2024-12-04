using UnityEngine;
using UnityEngine.UI;

namespace HT
{
    public class UI_Countdown_View : UI_Views
    {
        public RectTransform rect;
        [SerializeField] private Image countDown;

        public void UpdateView(float val, float max) => countDown.fillAmount = val / max;
    }
}
