using System.Collections;
using Zenject;

public class ShowChangeLanguageScreenButton : ButtonInteractionHandler
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
        StartCoroutine(ActivateButtonMethodCoroutine(_settingsUI.ShowPanel_ChangeLanguagePanel));
    }
}
