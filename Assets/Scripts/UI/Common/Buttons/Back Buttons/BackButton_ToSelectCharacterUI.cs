using Zenject;

public class BackButton_ToSelectCharacterUI : ButtonInteractionHandler
{
    private MainUI _mainUI;

    #region Zenject
    [Inject]
    private void Construct(MainUI mainUI)
    {
        _mainUI = mainUI;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        ShowAnimation_ButtonPressed();
        StartCoroutine(ActivateDelayedButtonMethodCoroutine(_mainUI.ShowSelectCharacterUI));
    }
}
