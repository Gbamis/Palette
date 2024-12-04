using UnityEngine;
using UnityEngine.UI;

namespace HT
{
    public class UI_CoinAdded_View : UI_Views
    {
        [SerializeField] private Text _amount;

        public void SetVallue(int amount) => _amount.text = amount > 0 ? "+" + amount.ToString() : "" + amount.ToString();
    }
}
