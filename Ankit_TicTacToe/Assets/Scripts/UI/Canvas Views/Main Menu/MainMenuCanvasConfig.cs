public class MainMenuCanvasConfig : BaseCanvasConfig
{
    public override CanvasViewType Type => CanvasViewType.MAIN_MENU;

    public bool ShowOptionToContinueExistingGame { 
        get;
        private set;
    }

    public MainMenuCanvasConfig(bool showOptionToContinueExistingGame)
    {
        ShowOptionToContinueExistingGame = showOptionToContinueExistingGame;
    }
}
