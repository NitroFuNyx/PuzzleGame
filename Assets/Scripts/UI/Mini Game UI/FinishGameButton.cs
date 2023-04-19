using Zenject;

public class FinishGameButton : ButtonInteractionHandler
{
    private MainUI _mainUI;
    private ResourcesManager _resourcesManager;
    private CurrentGameManager _currentGameManager;
    private AdsManager _adsManager;

    #region Zenject
    [Inject]
    private void Construct(MainUI mainUI, ResourcesManager resourcesManager, CurrentGameManager currentGameManager, AdsManager adsManager)
    {
        _mainUI = mainUI;
        _resourcesManager = resourcesManager;
        _currentGameManager = currentGameManager;
        _adsManager = adsManager;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        ShowAnimation_ButtonPressed();
        if(_currentGameManager.CurrentGameType == GameLevelTypes.MiniGame)
        {
            _adsManager.SetAdCanBeShownState();
        }
        StartCoroutine(ActivateDelayedButtonMethodCoroutine(_mainUI.ShowSelectGameLevel));
    }
}
