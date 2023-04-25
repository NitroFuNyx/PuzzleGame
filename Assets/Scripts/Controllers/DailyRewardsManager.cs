using UnityEngine;
using Zenject;

public class DailyRewardsManager : MonoBehaviour, IDataPersistance
{
    private int newDayPlaying;

    private bool newGameReward = false;

    private DataPersistanceManager _dataPersistanceManager;

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

    public void LoadData(GameData data)
    {
        if(newGameReward)
        {
            Debug.Log($"Get Reward New Game");
        }
        else
        {
            CheckDayOfPlaying(data.lastDayPlaying);
        }
    }

    public void SaveData(GameData data)
    {
        if(data.lastDayPlaying == DateConstants.newGameIndexForData)
        {
            newGameReward = true;
        }

        data.lastDayPlaying = System.DateTime.Now.DayOfYear;
    }

    private void CheckDayOfPlaying(int lastDay)
    {
        if (lastDay != System.DateTime.Now.DayOfYear)
        {
            Debug.Log($"Reward");
        }
        else
        {
            Debug.Log($"Same Day");
        }
    }
}
