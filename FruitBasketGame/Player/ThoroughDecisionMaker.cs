namespace FruitBasketGame
{
    internal class ThoroughDecisionMaker : DecisionMaker
    {
        public ThoroughDecisionMaker(int min, int max) : base(min, max)
        {
        }
        
        public override int GetNextNumber()
        {
            return ExtractVariantAt(0);
        }
    }
}
