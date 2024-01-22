using TMPro;
using UnityEngine;

public class GameBoardCanvas : BaseCanvas
{
    [SerializeField] private GameBoardController _boardController;
    [SerializeField] private TextMeshProUGUI gameStatusText;
    
    private GameBoardCanvasConfig TypedConfig => Config as GameBoardCanvasConfig;
    
    public override void Setup(BaseCanvasConfig config)
    {
        EventManager.RemoveEventListener<GameBoardPieceSet>(OnGameBoardPieceSet);
        EventManager.AddEventListener<GameBoardPieceSet>(OnGameBoardPieceSet);
        base.Setup(config);
        SetupView();
    }

    public void OnExitGameButtonClicked()
    {
        GameExitRequest request = new GameExitRequest();
        EventManager.TriggerEvent(request);
    }

    private void SetupView()
    {
        _boardController.ResetBoard(true);
        if (TypedConfig == null) return;
        
        _boardController.SetupInitialBoard(TypedConfig.BoardState);
        SetGameStatus(TypedConfig.BoardState.GetProgressState(), TypedConfig.BoardState.TurnState);
    }

    private void OnGameBoardPieceSet(GameBoardPieceSet evt)
    {
        SetGameStatus(evt.ProgressState, evt.TurnState);
    }

    private void SetGameStatus(GameProgressState progressState, GameTurnState turnState)
    {
        if (progressState == GameProgressState.InProgress)
        {
            gameStatusText.text = "Player " + (turnState == GameTurnState.Player1 ? "1" : "2") + "'s Move";
        }
        else if (progressState == GameProgressState.Complete)
        {
            gameStatusText.text = "Player " + (turnState == GameTurnState.Player1 ? "2" : "1") + " WON";
        }
        else
        {
            gameStatusText.text = "DRAW";
        }
    }
}
