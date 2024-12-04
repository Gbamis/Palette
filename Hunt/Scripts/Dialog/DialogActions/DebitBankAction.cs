using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    [CreateAssetMenu(fileName = "DialogActionHolder", menuName = "Games/Hunt/Dialog/Actions/DialogActionHolder")]
    public class DebitBankAction : DialogAction
    {
        //public GameEvent gameEvent;
        public BankAccount bankAccount;
        public int amount;
        private Action action;

        public override void InjectFunc(Action m_action)
        {
            base.InjectFunc(action);
            action = m_action;
        }

        public override void OnExecuteDialogAction()
        {
            bankAccount.Credit(amount);
            action.Invoke();
            //gameEvent.OnGetQuestSystem().CurrentQuest = quest;
        }
    }
}
