using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameBoardButton : MonoBehaviour
{
    [SerializeField, Range(0, GameBoardState.GridSize - 1)] private int rowIndex;
    [SerializeField, Range(0, GameBoardState.GridSize - 1)] private int colIndex;
    [SerializeField] private TextMeshProUGUI stateText;
    [SerializeField] private Button selectableButton;

    public Action<int, int> OnButtonClicked;

    public void SetButtonState(GamePieceState state, bool disable = false)
    {
        switch (state)
        {
            case GamePieceState.XPiece:
                stateText.text = "X";
                SetButtonEnabled(false);
                break;
            case GamePieceState.OPiece:
                stateText.text = "O";
                SetButtonEnabled(false);
                break;
            case GamePieceState.Empty:
                stateText.text = "";
                SetButtonEnabled(true);
                break;
            default:
                stateText.text = "";
                SetButtonEnabled(true);
                break;
        }

        if (disable) DisableButton();
    }

    public void DisableButton()
    {
        SetButtonEnabled(false);
    }

    public void OnBoardButtonClicked()
    {
        OnButtonClicked?.Invoke(rowIndex, colIndex);
    }
    
    private void SetButtonEnabled(bool enabled)
    {
        selectableButton.interactable = enabled;
    }
}
