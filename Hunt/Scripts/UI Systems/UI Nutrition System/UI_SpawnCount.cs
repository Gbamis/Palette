using UnityEngine;
using UnityEngine.UI;

namespace HT
{
    public class UI_SpawnCount : UI_Views
    {
        [SerializeField] private RectTransform rect;
        [SerializeField] private  Text value;

        public void FollowMouse(Vector3 end){
            Vector2 pos = Camera.main.WorldToScreenPoint(end);
            rect.position = pos;
        }
        public void SetData(float count, float total)
        {
            value.text = count.ToString() + "/" + total.ToString();
        }
    }
}
