using UnityEngine;
using Zenject;

public class AdsManager : MonoBehaviour, IDataPersistance
{
    private DataPersistanceManager _dataPersistanceManager;

    private bool _adCanBeShown = false;

    private void Awake()
    {
        _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
    }

    #region Zenject
    [Inject]
    private void Construct(DataPersistanceManager dataPersistanceManager)
    {
        _dataPersistanceManager = dataPersistanceManager;
    }
    #endregion Zenject

    public bool NeedToShowAdBeforeLevelStart()
    {
        bool showAd = false;

        int adShowIndex;

        adShowIndex = Random.Range(0, 2);

        if(adShowIndex == 1 && _adCanBeShown)
        {
            showAd = true;
        }

        return showAd;
    }

    public void SetAdCanBeShownState()
    {
        if(!_adCanBeShown)
        {
            _adCanBeShown = true;
            _dataPersistanceManager.SaveGame();
        }
    }

    public void LoadData(GameData data)
    {
        _adCanBeShown = data.adCanBeShown;
    }

    public void SaveData(GameData data)
    {
        data.adCanBeShown = _adCanBeShown;
    }
}
