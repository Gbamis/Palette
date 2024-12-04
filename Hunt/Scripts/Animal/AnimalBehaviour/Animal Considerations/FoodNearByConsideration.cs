using UnityEngine;

namespace HT
{
    [CreateAssetMenu(fileName = "food_near_Consideration", menuName = "Games/Hunt/AI/AnimalConsideration/FoodNear")]
    public class FoodNearByConsideration : AnimalConsiderations
    {
        public float searchRadius;
        public LayerMask foodLayer;
        public Color debugColor;
        public bool showDebug;


        public override float CalculateScore(AnimalStat animalStat) => FoundFood(animalStat) ? 1 : 0;

        private bool FoundFood(AnimalStat animalStat)
        {
            if (showDebug)
            {
                Routine.Instance.AddInfo(new DebugCheck()
                {
                    radius = searchRadius,
                    debugColor = debugColor,
                    center = animalStat.animaTransform.position
                });
            }


            Collider[] colliders = new Collider[10];
            Physics.OverlapSphereNonAlloc(animalStat.animaTransform.position, searchRadius, colliders, foodLayer);
            int rand = Random.Range(0,9);
            if (colliders[rand] != null)
            {
                animalStat.place_of_action = colliders[rand].transform;
                return true;
            }


            return false;
        }
    }

}