using UnityEngine;

namespace HT
{
    [CreateAssetMenu(fileName = "inverse_Consideration", menuName = "Games/Hunt/AI/AnimalConsideration/Inverse")]
    public class InverseConsideration : AnimalConsiderations
    {
        public float score;
        public float food;
        public AnimalConsiderations animalConsiderations;
        public override float CalculateScore(AnimalStat animalStat)
        {
            food = animalConsiderations.CalculateScore(animalStat);
            score = 1 - food;
            return score;
        }
    }

}