using System.Collections;
using UnityEngine;
using System;
using Zenject;

public class ClueButton : ButtonInteractionHandler
{
    private PuzzleGamesEnvironmentsHolder _puzzleGamesEnvironmentsHolder;

    private CurrentGameManager _currentGameManager;
    private RewardedAdsButton rewardedAdsButton;

    private float buttonStatusResetDelay = 0.3f;

    private void Awake()
    {
        if(TryGetComponent(out RewardedAdsButton button))
        {
            rewardedAdsButton = button;
            rewardedAdsButton.SetStartData();
        }
    }

    private void Start()
    {
        if(rewardedAdsButton)
            rewardedAdsButton.OnRewardReadyToBeGranted += ShowClue;
    }

    private void OnDestroy()
    {
        if (rewardedAdsButton)
            rewardedAdsButton.OnRewardReadyToBeGranted -= ShowClue;
    }
  
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
        if(rewardedAdsButton)
        {
            rewardedAdsButton.ShowAd();
        }
    }

    private void ShowClue()
    {
        StartCoroutine(ActivateDelayedButtonMethodCoroutine(_puzzleGamesEnvironmentsHolder.CurrentlyActiveGame.CluesManager.ShowRandomClue));
    }

    private IEnumerator ResetButtonCoroutine()
    {
        yield return new WaitForSeconds(buttonStatusResetDelay);
        _currentGameManager.PuzzleUIButtonPressed = false;
    }
}
