namespace FruitBasketGame
{
    public class GameResult
    {
        public string UserName { get; set; }
        public int UserTry { get; set; }
        public bool UserWon { get; set; }
        public int TotalTries { get; set; }
        public EndGameReason EndGameReason { get; set; }
    }
}
