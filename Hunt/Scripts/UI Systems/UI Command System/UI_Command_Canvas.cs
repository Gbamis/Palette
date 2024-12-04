using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace HT
{
    public class UI_Command_Canvas : UICanvas
    {
        [SerializeField] private RectTransform centerRect;
        [SerializeField] private RectTransform commmandListView;
        [SerializeField] private UI_Command cmdPrefab;
        public Vector2 offset;
        public override void OnInit()
        {
            base.OnInit();
            centerRect.gameObject.SetActive(false);
        }

        public override void OnDisplay()
        {
            base.OnDisplay();

        }

        public override void OnHide()
        {
            base.OnHide();
        }

        public void ClearCommands()
        {
            if (commmandListView.childCount > 0)
            {
                foreach (Transform child in commmandListView) { Destroy(child.gameObject); }
            }
        }

        private void PositionCommandOnItem(Vector3 worldPos)
        {
            Vector2 pos = Camera.main.WorldToScreenPoint(worldPos);
            pos += offset;
            centerRect.position = pos;
        }

        public void SetItemsByFilter(List<AnimalAction> commands)
        {
            
        }

        public void SetItems()
        {

            ClearCommands();
        }

    }
}
