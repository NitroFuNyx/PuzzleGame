using Zenject;
using UnityEngine;

public class WatchVideoButton : ButtonInteractionHandler
{
    private ResourcesManager _resourcesManager;

    private RewardedAdsButton rewardedAdsButton;

    [SerializeField] private bool videoWatched = true;

    public bool VideoWatched { get => videoWatched; set => videoWatched = value; }

    private void Awake()
    {
        rewardedAdsButton = GetComponent<RewardedAdsButton>();
    }

    private void Start()
    {
        rewardedAdsButton.OnRewardReadyToBeGranted += GrandReward;
    }

    private void OnDestroy()
    {
        rewardedAdsButton.OnRewardReadyToBeGranted -= GrandReward;
    }

    #region Zenject
    [Inject]
    private void Construct(ResourcesManager resourcesManager)
    {
        _resourcesManager = resourcesManager;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        if (!videoWatched)
        {
            videoWatched = true;
            ShowAnimation_ButtonPressed();
            StartCoroutine(ActivateDelayedButtonMethodCoroutine(rewardedAdsButton.ShowAd));
        }
    }

    private void GrandReward()
    {
        _resourcesManager.AddCoinsAsAdReward();
    }
}
