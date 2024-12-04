using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    public class AnimalBehaviour : MonoBehaviour
    {
        public AnimalAction defaultAction;
        public AnimalAction currentBehavior;
        public List<AnimalAction> animalActions;
        private AnimalStat animalStat;

        public void Init(AnimalStat m_stat) => animalStat = m_stat;
        public void Die() => animalActions.Clear();

        public void AddAnimalAction(AnimalAction animalAction)
        {
            if (!animalActions.Contains(animalAction)) { animalActions.Add(animalAction); }
        }

        public void ClearActions() => animalActions.Clear();

        public AnimalAction DefaultAction()
        {
            currentBehavior = defaultAction;
            return currentBehavior;
        }

        public AnimalAction DecideBestAction()
        {
            float best_score = 0;
            foreach (AnimalAction action in animalActions)
            {
                if (CalculateActionScore(action) > best_score)
                {
                    best_score = action.action_score;
                    currentBehavior = action;
                }
            }
            if (currentBehavior == null) { currentBehavior = defaultAction; }
            return currentBehavior;
        }

        private float CalculateActionScore(AnimalAction action)
        {
            float score = 1f;
            foreach (AnimalConsiderations ac in action.animalConsiderations)
            {
                if (ac.CalculateScore(animalStat) == 0)
                {
                    action.action_score = 0;
                    return 0;
                }
                score *= ac.CalculateScore(animalStat);

            }
            float orign = score;
            float mod = 1 - (1 / action.animalConsiderations.Length);
            float makeup = (1 - orign) * mod;
            score = orign + (makeup * orign);
            action.action_score = score;
            return score;
        }
    }

}