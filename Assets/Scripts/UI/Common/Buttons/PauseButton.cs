using System.Collections;
using UnityEngine;
using Zenject;

public class PauseButton : ButtonInteractionHandler
{
    private MainUI _mainUI;
    private CurrentGameManager _currentGameManager;
    private PuzzleGamesEnvironmentsHolder _puzzleGamesEnvironments;

    private float buttonStatusResetDelay = 0.3f;

    #region Zenject
    [Inject]
    private void Construct(MainUI mainUI, CurrentGameManager currentGameManager, PuzzleGamesEnvironmentsHolder puzzleGamesEnvironmentsHolder)
    {
        _mainUI = mainUI;
        _currentGameManager = currentGameManager;
        _puzzleGamesEnvironments = puzzleGamesEnvironmentsHolder;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        _currentGameManager.PuzzleUIButtonPressed = true;
        StartCoroutine(ResetButtonCoroutine());
        _mainUI.ShowPauseUI();

        if(_currentGameManager.CurrentGameType == GameLevelTypes.Puzzle)
        {
            _puzzleGamesEnvironments.CurrentlyActiveGame.InputManager.ChangeCheckInputState(false);
        }
    }

    private IEnumerator ResetButtonCoroutine()
    {
        yield return new WaitForSeconds(buttonStatusResetDelay);
        _currentGameManager.PuzzleUIButtonPressed = false;
    }
}
