using UnityEngine;
using Zenject;

public class SelectCharacterButton : ButtonInteractionHandler
{
    [Header("Button Data")]
    [Space]
    [SerializeField] private CharacterTypes characterType;

    private CurrentGameManager _currentGameManager;
    private MainUI _mainUI;

    #region Zenject
    [Inject]
    private void Construct(CurrentGameManager currentGameManager, MainUI mainUI)
    {
        _currentGameManager = currentGameManager;
        _mainUI = mainUI;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        _currentGameManager.SetCurrentCharacter(characterType);
        ShowAnimation_ButtonPressed();
        StartCoroutine(ActivateDelayedButtonMethodCoroutine(_mainUI.ShowSelectGameLevel));
    }
}
