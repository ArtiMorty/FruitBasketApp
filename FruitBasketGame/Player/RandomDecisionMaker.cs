namespace FruitBasketGame
{
    internal class RandomDecisionMaker : DecisionMaker
    {
        public RandomDecisionMaker(int min, int max) : base(min, max)
        {
        }

        public override int GetNextNumber()
        {
            return _random.Next(_min, _max + 1);
        }
    }
}
