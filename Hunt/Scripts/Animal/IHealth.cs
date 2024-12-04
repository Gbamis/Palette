using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    public interface IHealth
    {
        void Vaccinate() { }
        void TriggerSickness(float rate) { }
    }

}