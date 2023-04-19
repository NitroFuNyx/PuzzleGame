using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SelectCharacterButton : ButtonInteractionHandler
{
    [Header("Button Data")]
    [Space]
    [SerializeField] private CharacterTypes characterType;

    private CurrentGameManager _currentGameManager;
    private MainUI _mainUI;
    private AdsManager _adsManager;

    private RewardedAdsButton rewardedAdsButton;
    private AdsInitializer _adsInitializer;

    private bool adActivated = false;

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
            rewardedAdsButton.OnRewardReadyToBeGranted += AddFinished_ExecuteReaction;
        }
        if (ButtonComponent == null)
        {
            ButtonComponent = GetComponent<Button>();
            ButtonComponent.onClick.AddListener(ButtonActivated);
            ButtonComponent.onClick.AddListener(PlayButtonInteractionSound);
        }
    }

    private void OnDestroy()
    {
        if (rewardedAdsButton)
            rewardedAdsButton.OnRewardReadyToBeGranted -= AddFinished_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(CurrentGameManager currentGameManager, MainUI mainUI, AdsInitializer adsInitializer, AdsManager adsManager)
    {
        _currentGameManager = currentGameManager;
        _mainUI = mainUI;
        _adsInitializer = adsInitializer;
        _adsManager = adsManager;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        if (rewardedAdsButton && _adsInitializer.AdsCanBeLoaded && _adsManager.NeedToShowAdBeforeLevelStart())
        {
            adActivated = true;
            rewardedAdsButton.ShowAd();
        }
        else
        {
            _currentGameManager.SetCurrentCharacter(characterType);
            ShowAnimation_ButtonPressed();
        }
        //StartCoroutine(ActivateDelayedButtonMethodCoroutine(_mainUI.ShowSelectGameLevel));
    }

    private void AddFinished_ExecuteReaction()
    {
        if(adActivated)
        {
            _currentGameManager.SetCurrentCharacter(characterType);
            ShowAnimation_ButtonPressed();
            adActivated = false;
        }
    }
}
