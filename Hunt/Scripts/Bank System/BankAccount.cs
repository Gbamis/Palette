using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace HT
{
    [CreateAssetMenu(fileName = "Bank Account", menuName = "Games/Hunt/Bank/Account")]
    public class BankAccount : ScriptableObject
    {
        public int account_balance;

        public Action<int> OnAccountChanged;

        public void Credit(int amount)
        {
            account_balance += amount;
            OnAccountChanged?.Invoke(amount);
        }
        

        public bool Debit(int amount)
        {
            if (account_balance >= amount)
            {
                account_balance -= amount;
                OnAccountChanged?.Invoke(-amount);
                return true;
            }
            return false;
        }
    }
}
