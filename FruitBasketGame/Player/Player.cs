using System.Threading;

namespace FruitBasketGame
{
    internal class Player
    {
        public Player(string name, PlayerType type, Game game)
        {
            Name = name;
            _game = game;
            _decisionMaker = DecisionMaker.Get(type, _game.MinBasketWeight, _game.MaxBasketWeight);

            _game.OnPlayerTriesToGuess += x => _decisionMaker.OnCompetitorTriesToGuess(x);
            _game.OnGameEnd += x => weightGuessed = true;
        }

        private readonly Game _game;
        private bool weightGuessed = false;

        public string Name { get; }

        private readonly DecisionMaker _decisionMaker;

        public void Start()
        {
            while (!weightGuessed)
            {
                var weight = _decisionMaker.GetNextNumber();
                int penalty;

                _game.TryToGuess(Name, weight, out penalty);

                if (penalty != 0)
                {
                    Thread.Sleep(penalty);
                }
            }
        }
    }
}
