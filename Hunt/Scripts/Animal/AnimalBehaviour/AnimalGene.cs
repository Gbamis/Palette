using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    [CreateAssetMenu(fileName = "GeneInfo", menuName = "Games/Hunt/Animal/Gene")]
    public class AnimalGene : ScriptableObject
    {
        public Color skinColor;
        [Range(1, 1.8f)] public float maxGrowthSize;
        [Range(0.001f, 0.004f)] public float tirednessRate;//idle 0.003f //move 0.06f
        [Range(0.005f, 0.009f)] public float reproductionRate;//Random.Range(0.005f,0.008f);
        //[Range(0, 0.1f)] public float heatReplenishRate;
        [Range(0.1f, 0.8f)] public float diseaseRate;
        public float red;
        public float green;
        public float blue;
    }
}

