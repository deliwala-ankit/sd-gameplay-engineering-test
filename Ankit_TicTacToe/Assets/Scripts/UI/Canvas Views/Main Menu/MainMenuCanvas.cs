using UnityEngine;

public class MainMenuCanvas : BaseCanvas
{
    [SerializeField] private GameObject continueOption;
    
    private MainMenuCanvasConfig TypedConfig => Config as MainMenuCanvasConfig;
    
    public override void Setup(BaseCanvasConfig config)
    {
        base.Setup(config);
        SetupView();
    }
    
    public void OnStartNewGameButtonClicked()
    {
        TriggerPlayGameRequest(true);
    }

    public void OnContinueExistingGameButtonClicked()
    {
        TriggerPlayGameRequest(false);
    }
    
    private void SetupView()
    {
        if (continueOption == null) return;
        continueOption.SetActive(TypedConfig?.ShowOptionToContinueExistingGame ?? false);
    }

    private void TriggerPlayGameRequest(bool startNewGame)
    {
        PlayGameRequest request = new PlayGameRequest(startNewGame);
        EventManager.TriggerEvent(request);
    }
}
