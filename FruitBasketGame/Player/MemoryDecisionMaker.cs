namespace FruitBasketGame
{
    internal class MemoryDecisionMaker : DecisionMaker
    {
        public MemoryDecisionMaker(int min, int max) : base(min, max)
        {
        }

        public override int GetNextNumber()
        {
            var index = _random.Next(_variants.Count - 1);
            var result = _variants[index];
            _variants.RemoveAt(index);

            return result;
        }
    }
}
