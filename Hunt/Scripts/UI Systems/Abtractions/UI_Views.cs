using UnityEngine;
using Zenject;

namespace HT
{
    public abstract class UI_Views : MonoBehaviour
    {
        [Inject] protected MouseMode mouseMode;
        [Inject] protected GameplayEvent gameplayEvent;
        [Inject] protected BankAccount bankAccount;
        [Inject] protected GlobalData globalData;
    }
}
