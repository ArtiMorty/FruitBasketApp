namespace FruitBasketGame
{
    public enum PlayerType
    {
        Random = 0,
        Memory,
        Thorough,
        Cheater,
        ThoroughCheater
    }

    public enum EndGameReason
    {
        PlayerWon = 0,
        TimeOut,
        MaxAttemptsReached
    }
}
