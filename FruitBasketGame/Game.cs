using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FruitBasketGame
{
    public class Game
    {
        public Game(int minWeight, int maxWeight, int maxTries, out Func<int> getBasketWeight)
        {
            _basket = new Basket(minWeight, maxWeight);
            _maxTries = maxTries;
            _locker = new object();
            _playersThreads = new List<Thread>();
            _players = new List<Player>();
            _triesCounter = 0;
            getBasketWeight = () => _basket.Weight;
        }

        private readonly int _triesCounter;
        private readonly int _maxTries;
        private Timer _timer;
        private readonly object _locker;
        private readonly Basket _basket;
        private readonly List<Thread> _playersThreads;
        private readonly List<Player> _players;
        private GameResult _result;

        public event Action<GameResult> OnGameEnd;
        public event Action<int> OnPlayerTriesToGuess;
        public int MinBasketWeight => _basket.MinWeight;
        public int MaxBasketWeight => _basket.MaxWeight;

        public void Start()
        {
            if (_players.Any())
            {
                foreach (var p in _players)
                {
                    var playerThread = new Thread(p.Start);
                    _playersThreads.Add(playerThread);
                    playerThread.Start();
                }

                _timer = new Timer(_ =>
                {
                    lock (_locker)
                    {
                        Stop(EndGameReason.TimeOut);
                    }
                }, null, 1500, Timeout.Infinite);

                foreach (var item in _playersThreads)
                {
                    item.Join();
                }
            }
            else
            {
                throw new InvalidOperationException("There are no players in the game");
            }
        }

        public void Stop(EndGameReason reason)
        {
            _timer.Dispose();
            _result.EndGameReason = reason;

            OnGameEnd?.Invoke(_result);

            foreach (var p in _playersThreads)
            {
                try
                {
                    p.Abort();
                }
                catch (ThreadAbortException)
                {

                }
            }
        }

        public void AddPlayer(string name, PlayerType type)
        {
            _players.Add(new Player(name, type, this));
        }

        public void TryToGuess(string playerName, int weight, out int penalty)
        {
            penalty = Math.Abs(_basket.Weight - weight);

            lock (_locker)
            {
                if(_triesCounter <= _maxTries)
                {
                    CheckAndSetResult(weight, playerName);

                    OnPlayerTriesToGuess?.Invoke(weight);

                    if (_basket.Weight == weight)
                    {
                        _result.UserWon = true;
                        
                        Stop(EndGameReason.PlayerWon);
                    }
                }
                else
                {
                    Stop(EndGameReason.MaxAttemptsReached);
                }
            }
        }

        private void CheckAndSetResult(int weight, string playerName)
        {
            if(_result != null)
            {
                if(Math.Abs(_basket.Weight - weight) < Math.Abs(_basket.Weight - _result.UserTry))
                {
                    _result.UserName = playerName;
                    _result.UserTry = weight;
                }
            }
            else
            {
                _result = new GameResult
                {
                    UserName = playerName,
                    UserTry = weight,
                    UserWon = false,
                    TotalTries = 1
                };

            }

            ++_result.TotalTries;
        }
    }
}
