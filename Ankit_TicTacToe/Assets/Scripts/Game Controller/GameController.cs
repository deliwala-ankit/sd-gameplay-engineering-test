using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private CanvasManager canvasManager;
    [SerializeField] private BaseGameStateStorageService storageService;

    private readonly GameBoardState _gameBoardState = new ();

    private void Awake()
    {
        EventManager.AddEventListener<PlayGameRequest>(HandlePlayGameRequest);
        EventManager.AddEventListener<GameBoardPieceSelectionTriggered>(HandleGameBoardPieceSelectionTrigger);
        EventManager.AddEventListener<GameBoardPieceSet>(HandleGameBoardPieceSet);
        EventManager.AddEventListener<GameExitRequest>(HandleGameExitRequest);
        
        _gameBoardState.ResetGameBoardState();
        canvasManager.ShowCanvasView(new MainMenuCanvasConfig(storageService.HasActiveState()));
    }

    private void OnDestroy()
    {
        EventManager.RemoveEventListener<PlayGameRequest>(HandlePlayGameRequest);
        EventManager.RemoveEventListener<GameBoardPieceSelectionTriggered>(HandleGameBoardPieceSelectionTrigger);
        EventManager.RemoveEventListener<GameBoardPieceSet>(HandleGameBoardPieceSet);
        EventManager.RemoveEventListener<GameExitRequest>(HandleGameExitRequest);
    }

    private void HandlePlayGameRequest(PlayGameRequest evt)
    {
        if (evt.NewGame || (_gameBoardState.TryParseBoardState(storageService.GetActiveGameState()) == false))
        {
            _gameBoardState.ResetGameBoardState();
            storageService.DeleteActiveGameState();
        }
        
        canvasManager.ShowCanvasView(new GameBoardCanvasConfig(_gameBoardState));
    }

    private void HandleGameBoardPieceSelectionTrigger(GameBoardPieceSelectionTriggered evt)
    {
        _gameBoardState.SetGamePieceAtCell(evt.RowIndex, evt.ColIndex);
    }
    
    private void HandleGameBoardPieceSet(GameBoardPieceSet evt)
    {
        if (evt.ProgressState == GameProgressState.InProgress)
        {
            storageService.SaveActiveGameState(_gameBoardState.StringifyBoardState());
        }
        else
        {
            storageService.DeleteActiveGameState();
        }
    }

    private void HandleGameExitRequest(GameExitRequest evt)
    {
        canvasManager.ShowCanvasView(new MainMenuCanvasConfig(storageService.HasActiveState()));
    }
}
