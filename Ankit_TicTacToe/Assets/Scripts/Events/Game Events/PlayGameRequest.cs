public class PlayGameRequest : BaseGameEvent
{
    public bool NewGame
    { 
        get; 
        private set;
    }

    public PlayGameRequest(bool newGame)
    {
        NewGame = newGame;
    }
}
