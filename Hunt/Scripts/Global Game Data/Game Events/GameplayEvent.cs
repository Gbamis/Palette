using UnityEngine;
using System;

namespace HT
{
    [CreateAssetMenu(fileName = " GamePlayEvents", menuName = "Games/Hunt/EventChannel/GameplayEvents")]
    public class GameplayEvent : ScriptableObject
    {

        public GAMESTATE gameState;
        public GameObject virusPrefab;
        public Func<AnimalGene, bool, Animal> OnCreateAnimalFromGene;
        public Action<IGoat, IGoat, Transform> OnBreedGoat;
        public Action<Color, Vector3> OnGoatProduced;
        public Func<bool> OnCheckIsBreedMaxed;
        public Action<int, int> OnBreedStatChanged;
        public Action<Animal> OnGoatKilled;

        public Action<Animal, Gender> OnAnimalBought;

        public Func<GameObject> OnGetObjectFromPool;

        public Func<float> OnGetHourNormalized;

        public Action OnShowSelectionRange;

        public Action<Vector3> OnUnit_Hover;
        public Action OnUnit_UnHover;
        public Action<ICommandable, Gender> OnUnit_Select;
        public Action OnUnit_Move;
    }

}