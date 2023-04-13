using Zenject;

public class BackButton_ToInfoPanel : ButtonInteractionHandler
{
    private SettingsUI _settingsUI;

    #region Zenject
    [Inject]
    private void Construct(SettingsUI settingsUI)
    {
        _settingsUI = settingsUI;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        ShowAnimation_ButtonPressed();
        StartCoroutine(ActivateDelayedButtonMethodCoroutine(_settingsUI.ShowPanel_InfoPanel));
    }
}
