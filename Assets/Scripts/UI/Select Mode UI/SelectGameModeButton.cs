using UnityEngine;
using Zenject;

public class SelectGameModeButton : ButtonInteractionHandler
{
    [Header("Button Data")]
    [Space]
    [SerializeField] private GameLevelTypes gameType;

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
        _currentGameManager.SetCurrentGameType(gameType);
        ShowAnimation_ButtonPressed();
        StartCoroutine(ActivateDelayedButtonMethodCoroutine(_mainUI.ShowSelectCharacterUI));
    }
}
