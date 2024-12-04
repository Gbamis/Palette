using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    public class Gene
    {
        public Color color;
        public float maxGrowthSize;
        public float tirednessRate;
        public float reproductionRate;
        public float heatReplenishRate;
        public float diseaseRate;
        public float red;
        public float green;
        public float blue;

        public string StringRepresentation() => JsonUtility.ToJson(this);
    }
}
