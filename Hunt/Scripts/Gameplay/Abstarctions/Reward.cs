using System;
using UnityEngine;

namespace HT
{
    public abstract class Reward
    {
        public abstract void ApplyReward();
    }

    public class AddCoinReward : Reward
    {
        private int m_coins;
        private Action<int> action;
        public AddCoinReward(Action<int> amount, int coins)
        {
            m_coins = coins;
            action = amount;
        }
        public override void ApplyReward() => action?.Invoke(m_coins);
    }

    public class UnlockNewBreedRward : Reward
    {
        public AnimalGene newBreadGene;
        public UnlockNewBreedRward(AnimalGene gene) => newBreadGene = gene;
        public override void ApplyReward() { }
    }

    public class UnclockNewShepherdReward : Reward
    {
        public override void ApplyReward() { }
    }

}