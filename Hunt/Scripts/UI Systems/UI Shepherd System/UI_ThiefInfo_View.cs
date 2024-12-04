using UnityEngine;
using UnityEngine.UI;

namespace HT
{
    public class UI_ThiefInfo_View : UI_Views
    {
        public RectTransform rect;
        [SerializeField] private Text hit;

        public void Set(int val) => hit.text = "Hit " + val.ToString();
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
