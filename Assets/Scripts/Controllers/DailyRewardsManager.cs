using UnityEngine;
using Zenject;

public class DailyRewardsManager : MonoBehaviour, IDataPersistance
{
    private int newDayPlaying;

    private bool newGameReward = false;
    private bool shouldGrandDailyReward = false;

    private DataPersistanceManager _dataPersistanceManager;

    public bool ShouldGrandDailyReward { get => shouldGrandDailyReward; private set => shouldGrandDailyReward = value; }

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
            shouldGrandDailyReward = true;
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
            shouldGrandDailyReward = true;
        }
        else
        {
            Debug.Log($"Same Day");
            shouldGrandDailyReward = false;
        }
    }
}
