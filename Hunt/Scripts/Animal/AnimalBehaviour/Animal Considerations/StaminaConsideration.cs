using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace HT
{
    [CreateAssetMenu(fileName = "Stamina_Consideration", menuName = "Games/Hunt/AI/AnimalConsideration/Stamina")]
    public class StaminaConsideration : AnimalConsiderations
    {
        [SerializeField] private AnimationCurve responseCurve;
        private float score = 0;

        public override void Awake() => score = 0;

        public override float CalculateScore(AnimalStat animalStat)
        {
            score = responseCurve.Evaluate(Mathf.Clamp01(animalStat.GetStamina()));
            return score;
        }
    }

}