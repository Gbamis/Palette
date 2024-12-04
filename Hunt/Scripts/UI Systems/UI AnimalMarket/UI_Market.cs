using UnityEngine;
using UnityEngine.UI;

namespace HT
{
    public class UI_Market : UICanvas
    {
        private Animal cart;
        public Text infoText;
        public GameObject marketTab;
        public Button buyButton;
        

        public override void OnInit()
        {
            base.OnInit();
            marketTab.SetActive(false);
            buyButton.onClick.AddListener(() =>
            {
                cart.Buy();
                marketTab.SetActive(false);
            });
        }

        public override void OnDisplay()
        {
            base.OnDisplay();
        }

        public override void OnHide()
        {
            base.OnHide();
            gameObject.SetActive(false);
        }
    }
}
