using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    public abstract class DialogAction : ScriptableObject
    {
        public virtual void InjectFunc(Action action) { }
        public abstract void OnExecuteDialogAction();
    }
}
