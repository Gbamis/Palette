using UnityEngine.UI;
using Zenject;

namespace HT
{
    public class UI_Coin_View : UI_Views
    {
        public Text amtText;

        public void UpdateAccount_UI()
        {
            if (amtText == null) { return; }
            amtText.text = bankAccount.account_balance.ToString();
        }
    }

}