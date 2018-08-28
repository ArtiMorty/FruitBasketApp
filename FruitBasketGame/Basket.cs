using System;

namespace FruitBasketGame
{
    internal class Basket
    {
        public Basket(int minWeight, int maxWeight)
        {
            MinWeight = minWeight;
            MaxWeight = maxWeight;
            Weight = new Random(DateTime.Now.Millisecond).Next(minWeight, maxWeight + 1);
        }

        public int MinWeight { get; }
        public int MaxWeight { get; }
        public int Weight;
    }
}