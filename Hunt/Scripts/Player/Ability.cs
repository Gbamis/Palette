using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    public abstract class Ability : ScriptableObject
    {
        protected PlayerConfig config;
        
        public virtual void OnInit(PlayerConfig playerConfig) => config = playerConfig;
        public virtual void OnReset(){}
        public virtual void OnUpdate() { }
        public virtual void OnFixedUpdate() { }
        public virtual void OnLateUpdate() { }
    }

}