using UnityEngine;
using UnityEngine.UI;

namespace HT
{
    public class UI_Goat_Info_View : UI_Views
    {
        public RectTransform rect;
        [SerializeField] private Text gender;
        [SerializeField] private Image staminaLevel;
        [SerializeField] private Image oesrusLevel;
        [SerializeField] private Vector2 offset;

        public void SetData(GoatInfoModel goatInfo)
        {
            if (gender == null) { return; }
            gender.text = goatInfo.gendder;
            staminaLevel.fillAmount = goatInfo.stamina;
            oesrusLevel.fillAmount = goatInfo.oestrus;

            Vector2 screen = Camera.main.WorldToScreenPoint(goatInfo.screenPos);
            screen.x += offset.x;
            screen.y += offset.y;

            rect.position = screen;
        }
    }
}
