using System;
using FruitBasketGame;

namespace FruitBasketApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<int> getBasketWeight;

            var game = new Game(40, 140, 100, out getBasketWeight);
            game.OnGameEnd += result => PrintGameResult(result);

            InitGame(game);

            Console.WriteLine($"Busket weight is {getBasketWeight()}");
            
            game.Start();

            Console.ReadKey();
        }

        public static void PrintGameResult(GameResult result)
        {
            if (result.UserWon)
            {
                Console.WriteLine($"The winer is {result.UserName}.");
            }
            else
            {
                Console.WriteLine($"{result.UserName} has been close with {result.UserTry}.");
            }
        }

        public static void InitGame(Game game)
        {
            Console.Write("Players quantity(2-8): ");
            var maxPlayers = ReadDigit(8, 2);

            Console.WriteLine();

            for (int i = 0; i < maxPlayers; i++)
            {
                int playerType = 0;
                string playerName = null;

                Console.Write($"Enter player {i + 1} name: ");
                playerName = ReadString();

                Console.Write($"{playerName} type(0 - Random, 1 - Memory, 2 - Thorough, 3 - Cheater, 4 - Thorough Cheater): ");
                playerType = ReadDigit(4);

                Console.WriteLine();

                game.AddPlayer(playerName, (PlayerType)playerType);
            }
        }

        static int ReadDigit(int max, int min = 0)
        {
            bool numberIsValid = false;
            int playersNumber = -1;


            while (!numberIsValid)
            {
                int.TryParse(Console.ReadKey().KeyChar.ToString(), out playersNumber);
                Console.WriteLine();
                numberIsValid = playersNumber >= min && playersNumber <= max;

                if (!numberIsValid)
                {
                    Console.Write("Try again: ");
                }
            };

            return playersNumber;
        }

        static string ReadString()
        {
            string result = null;
            bool resultIsValid = false;

            do
            {
                result = Console.ReadLine();
                resultIsValid = !string.IsNullOrEmpty(result);
                if (!resultIsValid)
                {
                    Console.Write("Try again: ");
                }
            }
            while (!resultIsValid);

            return result;
        }
    }
}
