using UnityEngine;
using System.Collections.Generic;
using System;
using Zenject;

namespace HT
{
    public class Goat_Breeding_Processor : MonoBehaviour
    {
        private List<Reward> reward;
        private Action OnCompleted;
        private const float visibleDiff = 0.1906845f;
        private int colorCount = 0;
        private readonly List<Color> possible_breed_colors = new();


        [Inject] private readonly GameplayEvent gameplayEvent;
        [Inject] private BankAccount bankAccount;

        [SerializeField] private UI_ColorStack_View ui_ColorStack_View;
        [SerializeField] private BreedingSystem breedingSystem;



        private void Awake() => ui_ColorStack_View.gameObject.SetActive(false);
        private void OnEnable()
        {
            gameplayEvent.OnGoatProduced += Event_GoatProduced;
        }

        private void OnDisable()
        {
            gameplayEvent.OnGoatProduced -= Event_GoatProduced;
        }

        private void Event_GoatProduced(Color color, Vector3 pos)
        {
            if (colorCount == 0)
            {
                CompleteObjective();
                return;
            }
            ui_ColorStack_View.CheckIfColorAtTopMatch(color, visibleDiff, ApplyReward);
        }



        public void StartObjectiveProcessing(Objective_BreedGoat obj, Action completeCallback)
        {
            ui_ColorStack_View.gameObject.SetActive(true);

            reward = obj.reward;
            OnCompleted = completeCallback;

            SelectParents();
        }

        private void ApplyReward()
        {
            colorCount--;
            bankAccount.Credit(5);
        }

        private void CompleteObjective()
        {
            ui_ColorStack_View.gameObject.SetActive(false);
            foreach (Reward rwd in reward) { rwd.ApplyReward(); }
            OnCompleted?.Invoke();
        }

        private void SelectParents()
        {
            possible_breed_colors.Clear();

            for (int i = 0; i < breedingSystem.animalGenes.Count; i++)
            {
                Color a = breedingSystem.animalGenes[i].skinColor;
                for (int j = 0; j < breedingSystem.animalGenes.Count; j++)
                {
                    Color b = breedingSystem.animalGenes[j].skinColor;
                    possible_breed_colors.Add(Full(a, b));
                }
            }
            colorCount = possible_breed_colors.Count;
            ui_ColorStack_View.CreateColorItems(possible_breed_colors);
        }

        private Color Full(Color m, Color f)
        {
            Color col;
            col.r = (m.r + f.r) * .5f; col.g = (m.g + f.g) * .5f; col.b = (m.b + f.b) * .5f;
            col.a = 1;
            return col;


        }

    }

}