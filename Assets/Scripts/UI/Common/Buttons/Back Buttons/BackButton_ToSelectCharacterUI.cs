using Zenject;
using UnityEngine;

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
        Debug.Log($"Mini Game End {gameObject}");
        StartCoroutine(ActivateDelayedButtonMethodCoroutine(_mainUI.ShowSelectCharacterUI));
    }
}
