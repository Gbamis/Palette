using UnityEngine;
using UnityEngine.EventSystems;

namespace HT
{
    public class NPC_PenOwner : NPC_Base, IamNPC
    {
        
        public int amount;
        public BankAccount bankAccount;

        public DialogAction dialogAction;
       
        public void OnSpanwed(Vector3 pos, Quaternion rot)
        {
            dialogAction.InjectFunc(NPC_Action);
            transform.SetPositionAndRotation(pos, rot);
            PositionIcon();
        }

        private void NPC_Action()
        {
            Debug.Log("NPC do work");
        }

        private void GetPaid()
        {
           bankAccount.Credit(amount);
        }
    }

}