using UnityEngine;
using Zenject;

public class ClueButton : ButtonInteractionHandler
{
    protected PuzzleGamesEnvironmentsHolder _puzzleGamesEnvironmentsHolder;

    #region Zenject
    [Inject]
    private void Construct(PuzzleGamesEnvironmentsHolder puzzleGamesEnvironmentsHolder)
    {
        _puzzleGamesEnvironmentsHolder = puzzleGamesEnvironmentsHolder;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        Debug.Log($"Clues");
        ShowAnimation_ButtonPressed();
        StartCoroutine(ActivateDelayedButtonMethodCoroutine(_puzzleGamesEnvironmentsHolder.CurrentlyActiveGame.CluesManager.ShowRandomClue));
    }
}
