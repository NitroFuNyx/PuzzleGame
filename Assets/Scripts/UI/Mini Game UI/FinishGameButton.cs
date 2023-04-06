using Zenject;

public class FinishGameButton : ButtonInteractionHandler
{
    private MainUI _mainUI;
    private ResourcesManager _resourcesManager;

    #region Zenject
    [Inject]
    private void Construct(MainUI mainUI, ResourcesManager resourcesManager)
    {
        _mainUI = mainUI;
        _resourcesManager = resourcesManager;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        ShowAnimation_ButtonPressed();
        StartCoroutine(ActivateDelayedButtonMethodCoroutine(_mainUI.ShowSelectGameLevel));
    }
}
