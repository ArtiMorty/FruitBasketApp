using System;
using System.Collections.Generic;

namespace FruitBasketGame
{
    internal abstract class DecisionMaker
    {
        public DecisionMaker(int min, int max)
        {
            _min = min;
            _max = max;
            _random = new Random(DateTime.Now.Millisecond);
            _variants = new List<int>();

            for (int i = min; i < max; i++)
            {
                _variants.Add(i);
            }
        }

        protected readonly int _min;
        protected readonly int _max;
        protected readonly Random _random;
        protected readonly List<int> _variants;

        public abstract int GetNextNumber();

        protected int ExtractVariantAt(int index)
        {
            var result = _variants[index];
            _variants.RemoveAt(index);

            return result;
        }

        public virtual void OnCompetitorTriesToGuess(int weight)
        {
            
        }

        public static DecisionMaker Get(PlayerType type, int min, int max)
        {
            switch (type)
            {
                case PlayerType.Random:
                    return new RandomDecisionMaker(min, max);
                case PlayerType.Memory:
                    return new MemoryDecisionMaker(min, max);
                case PlayerType.Thorough:
                    return new ThoroughDecisionMaker(min, max);
                case PlayerType.Cheater:
                    return new CheaterDecisionMaker(min, max);
                case PlayerType.ThoroughCheater:
                    return new ThoroughCheaterDecisionMaker(min, max);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
