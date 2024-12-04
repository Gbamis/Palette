using UnityEngine;
using Zenject;

namespace HT
{
    public class UICanvas : MonoBehaviour
    {
        [Inject] protected AppEvent appEvent;
        [Inject] protected UIEvents uiEvent;
        [Inject] protected GameplayEvent gameplayEvent;
        [Inject] protected BankAccount bankAccount;


        public virtual void OnInit() { }
        public virtual void OnDisplay() { }
        public virtual void OnHide() { }
    }
}
