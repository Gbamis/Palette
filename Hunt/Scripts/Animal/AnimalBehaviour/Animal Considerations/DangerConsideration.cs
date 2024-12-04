using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    [CreateAssetMenu(fileName = "Danger_Consideration", menuName = "Games/Hunt/AI/AnimalConsideration/Danger")]
    public class DangerConsideration : AnimalConsiderations
    {
        public Color debugColor;
        public float score;
        public LayerMask dangerLayer;
        public float searchRadius;
        [SerializeField] private bool showDebug;

        public override float CalculateScore(AnimalStat animalStat) => DangerNeaar(animalStat) ? 1 : 0;

        private bool DangerNeaar(AnimalStat animalStat)
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

            Collider[] colliders = new Collider[1];
            Physics.OverlapSphereNonAlloc(animalStat.animaTransform.position, searchRadius, colliders, dangerLayer);
            if (colliders[0] != null)
            {
                animalStat.place_of_action = colliders[0].transform;
                score = 1;
                return true;
            }
            score = 0;
            return false;
        }
    }
}
