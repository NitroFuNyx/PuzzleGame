using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Advertisements; 
 
public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    [SerializeField] private List<RewardedAdsButton> rewardedAdsButtonsList = new List<RewardedAdsButton>();

    private string _gameId;
    private bool adsCanBeLoaded = true;

    public bool AdsCanBeLoaded { get => adsCanBeLoaded; private set => adsCanBeLoaded = value; }

    void Awake()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            adsCanBeLoaded = false;
        }
        else
        {
            adsCanBeLoaded = true;
            InitializeAds();
        }
    }
 
    public void InitializeAds()
    {
#if UNITY_IOS
        _gameId = _iOSGameId;
#elif UNITY_ANDROID
            _gameId = _androidGameId;
#elif UNITY_EDITOR
            _gameId = _androidGameId;
#endif
        //_gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
        //    ? _iOSGameId
        //    : _androidGameId;
       Advertisement.Initialize(_gameId, _testMode, this);
    }
 
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        //WRITE HERE ALL AD LOADINGS!!!

        for(int i = 0; i < rewardedAdsButtonsList.Count; i++)
        {
            rewardedAdsButtonsList[i].LoadAd();
        }
    }
 
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}