using Zenject;

public class BackButton_ToPuzzleGameMainMode : ButtonInteractionHandler
{
    private PuzzleGameUI _puzzleGameUI;

    #region Zenject
    [Inject]
    private void Construct(PuzzleGameUI puzzleGameUI)
    {
        _puzzleGameUI = puzzleGameUI;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        ShowAnimation_ButtonPressed();
        StartCoroutine(ActivateDelayedButtonMethodCoroutine(_puzzleGameUI.ShowMainModePanel));
    }
}
