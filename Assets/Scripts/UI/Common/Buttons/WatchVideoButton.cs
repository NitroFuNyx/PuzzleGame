using Zenject;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WatchVideoButton : ButtonInteractionHandler
{
    [Header("Button Image Data")]
    [Space]
    [SerializeField] private Color buttonInactiveColor;
    [SerializeField] private float buttonInactiveAlphaValue;
    [SerializeField] private float changeButtonStateDuration = 0.1f;

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
        {
            rewardedAdsButton.OnRewardReadyToBeGranted += GrandReward;
        }
        if(ButtonComponent == null)
        {
            ButtonComponent = GetComponent<Button>();
            ButtonComponent.onClick.AddListener(ButtonActivated);
        }
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
            if(rewardedAdsButton && _adsInitializer.AdsCanBeLoaded)
            {
                videoWatched = true;
                ButtonComponent.interactable = false;
                SetButtonImageInactive();
                ShowAnimation_ButtonPressed();
                StartCoroutine(ActivateDelayedButtonMethodCoroutine(rewardedAdsButton.ShowAd));
            }
            else
            {
                ButtonComponent.interactable = false;
            }
        }
    }

    public void ResetButton()
    {
        videoWatched = false;
        ButtonComponent.interactable = true;
        SetButtonImageActive();
    }

    private void GrandReward()
    {
        _resourcesManager.AddCoinsAsAdReward();
    }

    private void SetButtonImageInactive()
    {
        buttonImage.color = buttonInactiveColor;
        buttonImage.DOFade(buttonInactiveAlphaValue, changeButtonStateDuration);
    }

    private void SetButtonImageActive()
    {
        buttonImage.color = Color.white;
        buttonImage.DOFade(1f, changeButtonStateDuration);
    }
}
