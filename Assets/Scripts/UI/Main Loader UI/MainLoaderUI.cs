using UnityEngine;
using System;

public class MainLoaderUI : MainCanvasPanel
{
    [Header("Internal References")]
    [Space]
    [SerializeField] private MainLoaderProgressPanel progressPanel;

    private Action loaderAnimationFinishedCallback;

    private void Start()
    {
        SubscribeOnEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    public void StartLoadingAnimation(Action OnSuccessfulAnimationFinished)
    {
        progressPanel.ShowProgress();
        loaderAnimationFinishedCallback = OnSuccessfulAnimationFinished;
    }

    public void ResetUIData()
    {
        progressPanel.ResetImagesState();
    }

    private void SubscribeOnEvents()
    {
        progressPanel.OnLoaderAnimationFinished += LoaderAnimationFinished_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        progressPanel.OnLoaderAnimationFinished -= LoaderAnimationFinished_ExecuteReaction;
    }

    private void LoaderAnimationFinished_ExecuteReaction()
    {
        loaderAnimationFinishedCallback();
    }
}
