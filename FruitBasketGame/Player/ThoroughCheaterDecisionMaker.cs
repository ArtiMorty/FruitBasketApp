namespace FruitBasketGame
{
    internal class ThoroughCheaterDecisionMaker : ThoroughDecisionMaker
    {
        public ThoroughCheaterDecisionMaker(int min, int max) : base(min, max)
        {
        }

        public override void OnCompetitorTriesToGuess(int weight)
        {
            _variants.Remove(weight);
        }
    }
}
