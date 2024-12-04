using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    public interface IObjective
    {
        void OnStartObjetive(GamePlaySystem system);
    }

    public class Objective_BreedGoat : IObjective
    {
        public List<Reward> reward;
        public Objective_BreedGoat(List<Reward> _reward = null) => reward = _reward;
        public void OnStartObjetive(GamePlaySystem system) => system.External_GoatBreed_Process(this);

    }

    public class Objective_KillParrasite : IObjective
    {
        public readonly int xp_level;

        public List<Reward> reward;

        public Objective_KillParrasite(int playerXP, List<Reward> rewards)
        {
            xp_level = playerXP;
            reward = rewards;
        }

        public void OnStartObjetive(GamePlaySystem system) => system.External_Parrasite_Process(this);

    }

    public class Objective_Theives : IObjective
    {
        public int totalThieves;
        public int maxHit;
        public int minSpawnTime;
        public int maxSpawnTime;
        public List<Reward> reward;

        public Objective_Theives(int count, int _maxHit, int minTime, int maxTime, List<Reward> _reward = null)
        {
            totalThieves = count;
            maxHit = _maxHit;
            reward = _reward;
            minSpawnTime = minTime;
            maxSpawnTime = maxTime;
        }

        public void OnStartObjetive(GamePlaySystem system)
        {
            system.External_GoatThief_Process(this);
        }
    }

    /*public class KillPredatorObjective : IObjective
    {
        public readonly int totalPredatorCount;
        public Reward reward;

        public KillPredatorObjective(int count, Reward _reward = null)
        {
            totalPredatorCount = count;
            reward = _reward;
        }

        public void OnStartObjetive(GamePlaySystem system) => system.CreatePredators(totalPredatorCount);
    }*/

}