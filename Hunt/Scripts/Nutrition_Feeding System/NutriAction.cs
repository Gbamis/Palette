using UnityEngine;
using System;

namespace HT
{

    [CreateAssetMenu(fileName = "Nutri_Action", menuName = "Games/Hunt/Nutrition/NutriActions")]
    public class NutriAction : ScriptableObject
    {
        public int totalCount;
        protected int spawnCount;
        public Texture2D cursorIcon;
        public LayerMask layerMask;
    }
}
