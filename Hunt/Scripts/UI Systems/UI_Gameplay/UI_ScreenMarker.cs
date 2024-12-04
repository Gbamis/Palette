using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace HT
{
    public class UI_ScreenMarker : MonoBehaviour
    {
        private Transform target;
        private RectTransform self;
        [SerializeField] private Image temp;

        public void Init(Transform obj, Sprite icon = null)
        {
            target = obj;
            self = GetComponent<RectTransform>();
            if (icon != null)
            {
                temp.sprite = icon;
            }
        }

        private void Update()
        {
            if (target != null)
            {
                Vector2 pos = Camera.main.WorldToScreenPoint(target.position);
                pos.x = Mathf.Clamp(pos.x, 100, Screen.width - 10);
                pos.y = Mathf.Clamp(pos.y, 100, Screen.height - 10);
                self.position = pos;

                float dis = Vector3.Distance(Routine.Instance.player.position, target.position);
                Color color = temp.color;
                color.a = (dis < 10f) ? 0 : 1;
                temp.color = color;
            }
        }
    }
}
