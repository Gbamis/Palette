
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    public enum FILTER { SINGLE, DOUBLE, MULTIPLE }
    public abstract class AnimalAction : ScriptableObject
    {
        public string action_name;
        public float action_score;
        public Sprite icon;
        public List<FILTER> actionFilter;

        public AnimalConsiderations[] animalConsiderations;


        public virtual void Awake() => action_score = 0;

        public virtual void Execute(AnimalController animalController) { }



    }
}
