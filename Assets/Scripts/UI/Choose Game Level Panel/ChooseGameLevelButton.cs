using UnityEngine;
using Zenject;

public class ChooseGameLevelButton : ButtonInteractionHandler
{
    [Header("Button Data")]
    [Space]
    [SerializeField] private GameLevelTypes gameType;
    [SerializeField] private GameLevelStates levelState;
    [SerializeField] private int levelIndex;

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
        if(levelState != GameLevelStates.Locked)
        {
            ShowAnimation_ButtonPressed();
            _currentGameManager.ActivateGameLevelEnvironment(levelIndex);
            StartCoroutine(ActivateDelayedButtonMethodCoroutine(_mainUI.ShowGameLevelUI));
        }
    }

    public void SetButtonData(GameLevelStates gameLevelState, GameLevelTypes gameLevelType, int index)
    {
        levelState = gameLevelState;
        gameType = gameLevelType;
        levelIndex = index;
    }
}
