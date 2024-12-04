using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace HT
{
    public class UI_RadialListView : MonoBehaviour
    {
        public RectTransform rootrRect;
        public RectTransform centerRect;
        public float radius;

        private void OnEnable() => Redraw();
        public void OnValidate() => Redraw();

        public void AddItem(RectTransform item) => item.SetParent(centerRect);
        public void ClearItems()
        {
            if (centerRect.childCount > 0)
            {
                foreach (Transform child in centerRect) { Destroy(child.gameObject); }
            }
        }

        public void Redraw()
        {
            if (centerRect.childCount > 0)
            {
                foreach (Transform wt in centerRect)
                {
                    RectTransform rt = wt.GetComponent<RectTransform>();
                    rt.localScale = Vector3.zero;
                }

                StartCoroutine(Create());
            }
            IEnumerator Create()
            {
                int i = 0;
                foreach (Transform wr in centerRect)
                {
                    float x = Mathf.Cos(i * radius) * radius;
                    float y = Mathf.Sin(i * radius) * radius;
                    Vector2 pos = new(x, y);

                    wr.GetComponent<RectTransform>().localScale = Vector3.one;
                    wr.GetComponent<RectTransform>().anchoredPosition = pos;
                    wr.gameObject.SetActive(true);
                    i++;
                    yield return new WaitForSeconds(0.03f);
                }
            }
        }
    }
}
