public class GameBoardCanvasConfig : BaseCanvasConfig
{
    public override CanvasViewType Type => CanvasViewType.GAME_BOARD;

    public GameBoardState BoardState
    {
        get;
        private set;
    }

    public GameBoardCanvasConfig(GameBoardState boardState)
    {
        BoardState = boardState;
    }
}
