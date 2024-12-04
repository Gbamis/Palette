using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    public abstract class AnimalConsiderations : ScriptableObject
    {
        public virtual void Awake() { }
        public abstract float CalculateScore(AnimalStat animalStat);
    }
}
