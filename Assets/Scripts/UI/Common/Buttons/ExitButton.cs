using Zenject;

public class ExitButton : ButtonInteractionHandler
{
    private MainUI _mainUI;
    private CurrentGameManager _currentGameManager;

    #region Zenject
    [Inject]
    private void Construct(MainUI mainUI, CurrentGameManager currentGameManager)
    {
        _mainUI = mainUI;
        _currentGameManager = currentGameManager;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        _mainUI.ExitGameMode();
        _currentGameManager.FinishGameWithoutSaving();
    }
}
