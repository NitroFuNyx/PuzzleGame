using System.Collections;
using UnityEngine;
using Zenject;

public class ClueButton : ButtonInteractionHandler
{
    private PuzzleGamesEnvironmentsHolder _puzzleGamesEnvironmentsHolder;

    private CurrentGameManager _currentGameManager;

    private float buttonStatusResetDelay = 0.3f;

    #region Zenject
    [Inject]
    private void Construct(PuzzleGamesEnvironmentsHolder puzzleGamesEnvironmentsHolder, CurrentGameManager currentGameManager)
    {
        _puzzleGamesEnvironmentsHolder = puzzleGamesEnvironmentsHolder;
        _currentGameManager = currentGameManager;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        _currentGameManager.PuzzleUIButtonPressed = true;
        StartCoroutine(ResetButtonCoroutine());
        ShowAnimation_ButtonPressed();
        StartCoroutine(ActivateDelayedButtonMethodCoroutine(_puzzleGamesEnvironmentsHolder.CurrentlyActiveGame.CluesManager.ShowRandomClue));
    }

    private IEnumerator ResetButtonCoroutine()
    {
        yield return new WaitForSeconds(buttonStatusResetDelay);
        _currentGameManager.PuzzleUIButtonPressed = false;
    }
}
