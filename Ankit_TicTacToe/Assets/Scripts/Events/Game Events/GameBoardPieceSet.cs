public class GameBoardPieceSet : BaseGameEvent
{
    public int RowIndex
    {
        get;
        private set;
    }
    
    public int ColIndex
    {
        get;
        private set;
    }

    public GamePieceState PieceState
    {
        get;
        private set;
    }

    public GameProgressState ProgressState
    {
        get;
        private set;
    }

    public GameTurnState TurnState
    {
        get;
        private set;
    }
    
    public GameBoardPieceSet(int rowIndex, int colIndex, GamePieceState pieceState, GameProgressState progressState, GameTurnState turnState)
    {
        RowIndex = rowIndex;
        ColIndex = colIndex;
        PieceState = pieceState;
        ProgressState = progressState;
        TurnState = turnState;
    }
}
