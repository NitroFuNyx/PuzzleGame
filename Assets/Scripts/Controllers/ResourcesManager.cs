using UnityEngine;
using Zenject;

public class ResourcesManager : MonoBehaviour
{
    [Header("Coins")]
    [Space]
    [SerializeField] private int wholeCoinsAmount;
    [SerializeField] private int currentLevelCoinsAmount;

    private PlayerDataManager _playerDataManager;

    public int WholeCoinsAmount { get => wholeCoinsAmount; private set => wholeCoinsAmount = value; }

    private void Start()
    {
        SubscribeOnEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    #region Zenject
    [Inject]
    private void Construct(PlayerDataManager playerDataManager)
    {
        _playerDataManager = playerDataManager;
    }
    #endregion Zenject

    public void IncreaseCurrentLevelCoins(int coins)
    {
        currentLevelCoinsAmount += coins;
    }

    public void DecreaseCurrentLevelCoins(int coins)
    {
        currentLevelCoinsAmount -= coins;

        if (currentLevelCoinsAmount < 0)
            currentLevelCoinsAmount = 0;
    }

    public void AddCurrentLevelCoinsToWholeCoinsAmount()
    {
        wholeCoinsAmount += currentLevelCoinsAmount;
    }

    public void ResetCurrentLevelCoinsData()
    {
        currentLevelCoinsAmount = 0;
    }

    public void SaveLevelData()
    {
        AddCurrentLevelCoinsToWholeCoinsAmount();
    }

    private void SubscribeOnEvents()
    {
        _playerDataManager.OnPlayerMainDataLoaded += PlayerMainDataLoaded_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        _playerDataManager.OnPlayerMainDataLoaded -= PlayerMainDataLoaded_ExecuteReaction;
    }

    private void PlayerMainDataLoaded_ExecuteReaction()
    {
        wholeCoinsAmount = _playerDataManager.CurrentCoinsAmount;
    }
}
