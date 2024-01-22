using System.Collections.Generic;
using UnityEngine;

public class GameBoardController : MonoBehaviour
{
    [SerializeField] private List<GameBoardButton> buttons = new ();

    private void Awake()
    {
        for (int i = 0; i < GameBoardState.GridSize; ++i)
        {
            for (int j = 0; j < GameBoardState.GridSize; ++j)
            {
                buttons[i * GameBoardState.GridSize + j].OnButtonClicked += OnGameBoardButtonClicked;
            }
        }
    }

    private void OnDestroy()
    {
        EventManager.RemoveEventListener<GameBoardPieceSet>(OnGameBoardPieceSet);
        
        for (int i = 0; i < GameBoardState.GridSize; ++i)
        {
            for (int j = 0; j < GameBoardState.GridSize; ++j)
            {
                buttons[i * GameBoardState.GridSize + j].OnButtonClicked -= OnGameBoardButtonClicked;
            }
        }
    }

    public void SetupInitialBoard(GameBoardState boardState)
    {
        for (int i = 0; i < GameBoardState.GridSize; ++i)
        {
            for (int j = 0; j < GameBoardState.GridSize; ++j)
            {
                buttons[i * GameBoardState.GridSize + j].SetButtonState(boardState.GetGamePieceAtCell(i, j));
            }
        }
        
        EventManager.AddEventListener<GameBoardPieceSet>(OnGameBoardPieceSet);
    }

    public void ResetBoard(bool disable = false)
    {
        for (int i = 0; i < GameBoardState.GridSize; ++i)
        {
            for (int j = 0; j < GameBoardState.GridSize; ++j)
            {
                buttons[i * GameBoardState.GridSize + j].SetButtonState(GamePieceState.Empty, disable);
            }
        }
        
        EventManager.RemoveEventListener<GameBoardPieceSet>(OnGameBoardPieceSet);
    }

    private void OnGameBoardButtonClicked(int rowIndex, int colIndex)
    {
        GameBoardPieceSelectionTriggered evt = new GameBoardPieceSelectionTriggered(rowIndex, colIndex);
        EventManager.TriggerEvent(evt);
    }

    private void OnGameBoardPieceSet(GameBoardPieceSet evt)
    {
        buttons[evt.RowIndex * GameBoardState.GridSize + evt.ColIndex].SetButtonState(evt.PieceState);
        if (evt.ProgressState != GameProgressState.InProgress)
        {
            for (int i = 0; i < GameBoardState.GridSize; ++i)
            {
                for (int j = 0; j < GameBoardState.GridSize; ++j)
                {
                    buttons[i * GameBoardState.GridSize + j].DisableButton();
                }
            }
        }
    }
}
