using UnityEngine;
using UnityEngine.UI;

namespace HT
{
    public class UI_Dialog_View : UI_Views
    {
        public RectTransform rect;
        [SerializeField] private Text nameTxt;
        [SerializeField] private Text messageTxt;

        public void SetData(DialogModel model){
            nameTxt.text = model.name;
            messageTxt.text = model.message;
            Vector2 pos = Camera.main.WorldToScreenPoint(model.pos);
            rect.position = pos;
        }
    }

}