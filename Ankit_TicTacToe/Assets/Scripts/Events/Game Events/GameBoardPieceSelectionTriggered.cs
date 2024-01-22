public class GameBoardPieceSelectionTriggered : BaseGameEvent
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

    public GameBoardPieceSelectionTriggered(int rowIndex, int colIndex)
    {
        RowIndex = rowIndex;
        ColIndex = colIndex;
    }
}
