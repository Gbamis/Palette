using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    [System.Serializable]
    public class AnimalStat
    {
        public bool CanControl;

        [HideInInspector] public Gene gene;
        public Gender gender;

        [Range(0f, 100)][SerializeField] private float stamina;
        [Range(0.1f, 100)][SerializeField] private float heatLevel;

        [HideInInspector] public Transform animaTransform;
        public Transform place_of_action;

        public AnimalStat(Transform root, Gene m_gene, Gender m_gender)
        {
            stamina = 50;
            animaTransform = root;
            heatLevel = 100;
            gene = m_gene;
            gender = m_gender;
        }

        public float GetStamina() => stamina / 100;
        public float GetHeatLevel() => heatLevel / 100;
        public void ResetHeatLevel() => heatLevel = 0;
        public void AddStamina(float amount) => stamina = Mathf.Clamp(stamina += amount, 0.1f, 100);
        public void ReduceStamina(float multiplier = 0)
        {
            float mul = multiplier > 0 ? multiplier : 1;
            stamina = Mathf.Clamp(stamina -= gene.tirednessRate * mul, 0f, 100);
        }
        public void ResetStamina() => stamina = 100;
        public void ReplenishHeatLevel() => heatLevel = 100;


    }
}
