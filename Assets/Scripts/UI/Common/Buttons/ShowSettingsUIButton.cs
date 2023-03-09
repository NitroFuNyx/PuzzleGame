using System.Collections;
using UnityEngine;
using Zenject;

public class ShowSettingsUIButton : ButtonInteractionHandler
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
        StartCoroutine(ActivateButtonMethodCoroutine(_mainUI.ShowSettingsUI));
    }
}
