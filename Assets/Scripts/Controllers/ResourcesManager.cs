using UnityEngine;
using System;
using Zenject;

public class ResourcesManager : MonoBehaviour, IDataPersistance
{
    [Header("Coins")]
    [Space]
    [SerializeField] private int wholeCoinsAmount;
    [SerializeField] private int currentLevelCoinsAmount;

    private DataPersistanceManager _dataPersistanceManager;

    public int WholeCoinsAmount { get => wholeCoinsAmount; private set => wholeCoinsAmount = value; }
    public int CurrentLevelCoinsAmount { get => currentLevelCoinsAmount; private set => currentLevelCoinsAmount = value; }

    #region Events Declaration
    public event Action<int> OnLevelCoinsAmountChanged;
    public event Action<int> OnGeneralCoinsAmountChanged;
    #endregion Events Declaration

    #region Zenject
    [Inject]
    private void Construct(DataPersistanceManager dataPersistanceManager)
    {
        _dataPersistanceManager = dataPersistanceManager;
    }
    #endregion Zenject

    private void Awake()
    {
        _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
    }

    public void IncreaseCurrentLevelCoins(int coins)
    {
        currentLevelCoinsAmount += coins;
        OnLevelCoinsAmountChanged?.Invoke(currentLevelCoinsAmount);
    }

    public void DecreaseCurrentLevelCoins(int coins)
    {
        currentLevelCoinsAmount -= coins;

        if (currentLevelCoinsAmount < 0)
            currentLevelCoinsAmount = 0;

        OnLevelCoinsAmountChanged?.Invoke(currentLevelCoinsAmount);
    }

    public void AddCurrentLevelCoinsToWholeCoinsAmount()
    {
        wholeCoinsAmount += currentLevelCoinsAmount;
        OnGeneralCoinsAmountChanged?.Invoke(wholeCoinsAmount);
    }

    public void ResetCurrentLevelCoinsData()
    {
        currentLevelCoinsAmount = 0;
        OnLevelCoinsAmountChanged?.Invoke(currentLevelCoinsAmount);
    }

    public void SaveLevelData()
    {
        AddCurrentLevelCoinsToWholeCoinsAmount();
    }

    public void BuyLevel(int price)
    {
        wholeCoinsAmount -= price;
        _dataPersistanceManager.SaveGame();
        OnGeneralCoinsAmountChanged?.Invoke(wholeCoinsAmount);
    }

    public void LoadData(GameData data)
    {
        wholeCoinsAmount = data.currentCoinsAmount;
        OnGeneralCoinsAmountChanged?.Invoke(wholeCoinsAmount);
    }

    public void SaveData(GameData data)
    {
        data.currentCoinsAmount = wholeCoinsAmount;
    }
}
