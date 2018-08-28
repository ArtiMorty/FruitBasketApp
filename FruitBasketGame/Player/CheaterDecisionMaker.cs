namespace FruitBasketGame
{
    internal class CheaterDecisionMaker : RandomDecisionMaker
    {
        public CheaterDecisionMaker(int min, int max) : base(min, max)
        {
        }

        public override int GetNextNumber()
        {
            return ExtractVariantAt(_random.Next(_variants.Count));
        }

        public override void OnCompetitorTriesToGuess(int weight)
        {
            _variants.Remove(weight);
        }
    }
}
