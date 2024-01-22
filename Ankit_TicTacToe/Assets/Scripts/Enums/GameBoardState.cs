using System;

[Serializable]
public class GameBoardState
{
    public const int GridSize = 3;
    
    private readonly GamePieceState[,] _boardState = new GamePieceState[GridSize, GridSize]
    {
        { GamePieceState.Empty, GamePieceState.Empty, GamePieceState.Empty },
        { GamePieceState.Empty, GamePieceState.Empty, GamePieceState.Empty },
        { GamePieceState.Empty, GamePieceState.Empty, GamePieceState.Empty }
    };

    public GameTurnState TurnState { get; private set; } = GameTurnState.Player1;

    public GamePieceState GetGamePieceAtCell(int row, int col)
    {
        if ((row < 0) || ((row >= GridSize))) return GamePieceState.Empty;
        if ((col < 0) || ((col >= GridSize))) return GamePieceState.Empty;
        return _boardState[row, col];
    }

    public void SetGamePieceAtCell(int row, int col)
    {
        if ((row < 0) || ((row >= GridSize))) return;
        if ((col < 0) || ((col >= GridSize))) return;
        if ((GetProgressState() == GameProgressState.InProgress) && (_boardState[row, col] == GamePieceState.Empty))
        {
            if (TurnState == GameTurnState.Player1)
            {
                TurnState = GameTurnState.Player2;
                _boardState[row, col] = GamePieceState.XPiece;
            }
            else
            {
                TurnState = GameTurnState.Player1;
                _boardState[row, col] = GamePieceState.OPiece;
            }
            
            EventManager.TriggerEvent(new GameBoardPieceSet(row, col, _boardState[row, col], GetProgressState(), TurnState));
        }
    }

    public void ResetGameBoardState()
    {
        TurnState = GameTurnState.Player1;
        for (int i = 0; i < GridSize; ++i)
        {
            for (int j = 0; j < GridSize; ++j)
            {
                _boardState[i, j] = GamePieceState.Empty;
            }
        }
    }

    public GameProgressState GetProgressState()
    {
        for (int i = 0; i < GridSize; ++i)
        {
            if (HasRowWinner(i)) return GameProgressState.Complete;
        }
        
        for (int j = 0; j < GridSize; ++j)
        {
            if (HasColWinner(j)) return GameProgressState.Complete;
        }
        
        if (HasDiagonalWinner(true)) return GameProgressState.Complete;
        if (HasDiagonalWinner(false)) return GameProgressState.Complete;

        return HasEmptyCell() ? GameProgressState.InProgress : GameProgressState.Draw;
    }
    
    public bool TryParseBoardState(string stateString)
    {
        if (ValidateBoardStateString(stateString))
        {
            int xCount = 0;
            int oCount = 0;
            for (int i = 0; i < GridSize; ++i)
            {
                for (int j = 0; j < GridSize; ++j)
                {
                    int charIndex = i * GridSize + j;
                    if (stateString[charIndex] == ' ')
                    {
                        _boardState[i, j] = GamePieceState.Empty;
                    }
                    else if (stateString[charIndex] == 'X')
                    {
                        ++xCount;
                        _boardState[i, j] = GamePieceState.XPiece;
                    }
                    else if (stateString[charIndex] == 'O')
                    {
                        ++oCount;
                        _boardState[i, j] = GamePieceState.OPiece;
                    }
                }
            }
            TurnState = (xCount == oCount) ? GameTurnState.Player1 : GameTurnState.Player2;
            
            return true;
        }

        return false;
    }

    public string StringifyBoardState()
    {
        char[] stateString = new char[GridSize * GridSize];
        for (int i = 0; i < GridSize; ++i)
        {
            for (int j = 0; j < GridSize; ++j)
            {
                stateString[i * GridSize + j] = _boardState[i, j] switch
                {
                    GamePieceState.Empty => ' ',
                    GamePieceState.XPiece => 'X',
                    GamePieceState.OPiece => 'O',
                    _ => ' '
                };
            }
        }
        return new string(stateString);
    }
    
    private bool HasRowWinner(int row)
    {
        for (int j = 1; j < GridSize; ++j)
        {
            if (_boardState[row, j - 1] == _boardState[row, j]) continue;
            return false;
        }
        
        return (_boardState[row, 0] != GamePieceState.Empty);
    }
    
    private bool HasColWinner(int col)
    {
        for (int i = 1; i < GridSize; ++i)
        {
            if (_boardState[i - 1, col] == _boardState[i, col]) continue;
            return false;
        }
        
        return (_boardState[0, col] != GamePieceState.Empty);
    }

    private bool HasDiagonalWinner(bool forward)
    {
        int j = forward ? 1 : GridSize - 2;
        int jDelta = forward ? 1 : -1;
        for (int i = 1; i < GridSize; ++i, j += jDelta)
        {
            if (_boardState[i - 1, j - jDelta] == _boardState[i, j]) continue;
            return false;
        }
        
        if (forward)
        {
            return ((_boardState[0, 0] == GamePieceState.XPiece) || (_boardState[0, 0] == GamePieceState.OPiece));
        }
        return ((_boardState[0, GridSize - 1] == GamePieceState.XPiece) || (_boardState[0, GridSize - 1] == GamePieceState.OPiece));
    }

    private bool HasEmptyCell()
    {
        for (int i = 0; i < GridSize; ++i)
        {
            for (int j = 0; j < GridSize; ++j)
            {
                if (_boardState[i, j] == GamePieceState.Empty) return true;
            }
        }
        
        return false;
    }
    
    private bool ValidateBoardState()
    {
        int xCount = 0;
        int oCount = 0;
        for (int i = 0; i < GridSize; ++i)
        {
            for (int j = 0; j < GridSize; ++j)
            {
                switch (_boardState[i, j])
                {
                    case GamePieceState.XPiece:
                        ++xCount;
                        break;
                    case GamePieceState.OPiece:
                        ++oCount;
                        break;
                }
            }
        }
        
        return ((xCount - oCount) == 0) || ((xCount - oCount) == 1);
    }

    private bool ValidateBoardStateString(string stateString)
    {
        if (string.IsNullOrEmpty(stateString)) return false;
        if (stateString.Length != GridSize * GridSize) return false;
        
        int xCount = 0;
        int oCount = 0;
        for (int i = 0; i < GridSize; ++i)
        {
            for (int j = 0; j < GridSize; ++j)
            {
                if (stateString[i * GridSize + j] == ' ') continue;
                if (stateString[i * GridSize + j] == 'X')
                {
                    xCount += 1;
                    continue;
                }
                if (stateString[i * GridSize + j] == 'O')
                {
                    oCount += 1;
                    continue;
                }
                return false;
            }
        }
        
        return ((xCount - oCount) == 0) || ((xCount - oCount) == 1);
    }
}
