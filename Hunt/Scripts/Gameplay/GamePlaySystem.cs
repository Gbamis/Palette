using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace HT
{
    public class GamePlaySystem : MonoBehaviour
    {
        [Inject] private readonly AppEvent appEvent;

        private List<IObjective> objectives;
        public Goat_Breeding_Processor breeding_Processor;
        public Parasite_Processor parrasite_Processor;

        private void Start()
        {
            CreateGamePlay();
            appEvent.OnGameStarted += () =>
            {
                try
                {
                    objectives[0].OnStartObjetive(this);
                }
                catch (System.Exception e) { }
            };

        }

        private void CreateGamePlay()
        {
            objectives = new List<IObjective>();

            AddCoinReward add_20_coins = new(Reward_Add_Coins, 20);
            UnlockNewBreedRward unlock_green_breed = new(null);

            Objective_BreedGoat breed_three_Goats = new(new List<Reward>() { add_20_coins, unlock_green_breed });
            Objective_KillParrasite killParrasite = new(2, new List<Reward>() { add_20_coins });

            objectives.Add(breed_three_Goats);
            objectives.Add(killParrasite);
        }

        public void External_GoatBreed_Process(Objective_BreedGoat obj)
        {
            breeding_Processor.gameObject.SetActive(true);
            breeding_Processor.StartObjectiveProcessing(obj, NextObjective);
        }

        public void External_GoatThief_Process(Objective_Theives obj)
        {
        }

        public void External_Parrasite_Process(Objective_KillParrasite obj)
        {
            parrasite_Processor.StartObjectiveProcessing(obj, NextObjective);
        }

        private void Reward_Add_Coins(int amount)
        {
            Debug.Log("coinss aded");
        }

        public void CreatePredators(int count)
        {

        }

        public void NextObjective()
        {
            //objectives[1].OnStartObjetive(this);
        }
    }

}