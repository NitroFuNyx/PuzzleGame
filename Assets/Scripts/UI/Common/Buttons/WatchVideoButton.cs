using Zenject;
using UnityEngine;

public class WatchVideoButton : ButtonInteractionHandler
{
    private ResourcesManager _resourcesManager;

    private RewardedAdsButton rewardedAdsButton;
    private AdsInitializer _adsInitializer;

    [SerializeField] private bool videoWatched = true;

    public bool VideoWatched { get => videoWatched; set => videoWatched = value; }

    private void Awake()
    {
        if (TryGetComponent(out RewardedAdsButton button))
        {
            rewardedAdsButton = button;
            rewardedAdsButton.SetStartData();
        }
    }

    private void Start()
    {
        if (rewardedAdsButton)
            rewardedAdsButton.OnRewardReadyToBeGranted += GrandReward;
    }

    private void OnDestroy()
    {
        if (rewardedAdsButton)
            rewardedAdsButton.OnRewardReadyToBeGranted -= GrandReward;
    }

    #region Zenject
    [Inject]
    private void Construct(ResourcesManager resourcesManager, AdsInitializer adsInitializer)
    {
        _resourcesManager = resourcesManager;
        _adsInitializer = adsInitializer;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        if (!videoWatched)
        {
            videoWatched = true;
            ShowAnimation_ButtonPressed();
            if(rewardedAdsButton && _adsInitializer.AdsCanBeLoaded)
            {
                StartCoroutine(ActivateDelayedButtonMethodCoroutine(rewardedAdsButton.ShowAd));
            }
        }
    }

    private void GrandReward()
    {
        _resourcesManager.AddCoinsAsAdReward();
    }
}
