using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace HT
{
    public class UI_ShepherdUpgrade_View : UI_Views
    {
        private bool isRegistered;
        [SerializeField] private RectTransform rect;
        [SerializeField] private Button nextBtn;
        [SerializeField] private Button prevBtn;
        [SerializeField] private Text levelText;


        public void SetData(Action action, bool btn, ShepherdData data)
        {
            LevelText(data.attainedLevel);
            nextBtn.gameObject.SetActive(btn);

            if (btn)
            {
                if (!isRegistered)
                {
                    nextBtn.onClick.AddListener(() =>
                    {
                        action?.Invoke();
                        LevelText(data.attainedLevel);

                    });
                    isRegistered = true;
                }

            }
        }


        public void FollowTransform(Transform other)
        {
            Vector3 body = other.position;
            body.y += 5;
            Vector2 _pos = Camera.main.WorldToScreenPoint(body);
            rect.position = _pos;
        }

        private void LevelText(int level)
        {
            string lel = "L" + level.ToString() + " / L3";
            levelText.text = lel;
        }

        public void ClearView() => nextBtn.onClick.RemoveAllListeners();
    }
}
