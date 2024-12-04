using UnityEngine;
using System;
using Zenject;

namespace HT
{
    public abstract class NutrientPlacer : MonoBehaviour
    {
        [Inject] protected MouseMode mouseMode;
        [Inject] protected GameplayEvent gameplayEvent;
        
        public abstract void PlaceInScene(Action<int, int> OnUpdateQuantity,
        Action<Vector3> OnFollowMouse,
        Action OnCompleted);

    }
}
