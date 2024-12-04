using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace HT
{
    public class UI_Lumberjack_View : MonoBehaviour
    {
        public Vector2 offset;
        public Button actionBtn;
        public Texture2D activeCursor;
        public Texture2D inactiveCursor;
        private bool loop;

        public void AddAction(Action action, Transform root)
        {
            actionBtn.onClick.AddListener(() => action?.Invoke());

            loop = true;
            StartCoroutine(Follow());
            IEnumerator Follow()
            {
                while (loop)
                {
                    Vector2 _pos = Camera.main.WorldToScreenPoint(root.position);
                    _pos.x += offset.x;
                    _pos.y += offset.y;
                    actionBtn.GetComponent<RectTransform>().position = _pos;
                    yield return null;
                }
            }
        }

        public void RemoveAction()
        {
            actionBtn.onClick.RemoveAllListeners();
            loop = false;
        }
    }
}
