using System.Collections.Generic;
using UnityEngine;

public class KitchenMiniGameBonusTimersPanel : MonoBehaviour
{
    [Header("Panel Timers")]
    [Space]
    [SerializeField] private KitchenMiniGameBonusTimer bonusTimer_DoubleCoins;
    [SerializeField] private KitchenMiniGameBonusTimer bonusTimer_Shield;
    [SerializeField] private KitchenMiniGameBonusTimer bonusTimer_CoinsMagnet;
    [SerializeField] private List<KitchenMiniGameBonusTimer> bonusTimersList = new List<KitchenMiniGameBonusTimer>();

    private Dictionary<KitchenMiniGameItems, KitchenMiniGameBonusTimer> bonusTimersDictionary = new Dictionary<KitchenMiniGameItems, KitchenMiniGameBonusTimer>();

    private PlayerCollisionManager player;

    private void Awake()
    {
        FillTimersDictionary();
    }

    //private void Update()
    //{
    //    bonusTimer_DoubleCoins.UpdateTimer(player.CurrentDoubleCoinsBonusTime);
    //    bonusTimer_Shield.UpdateTimer(player.CurrentShieldBonusTime);
    //    bonusTimer_CoinsMagnet.UpdateTimer(player.CurrentCoinsMagnetBonusTime);
    //}

    public void UpdateBonusTimer(KitchenMiniGameItems itemType, float currentTime)
    {
        bonusTimersDictionary[itemType].UpdateTimer(currentTime);
    }

    public void CashComponents(PlayerCollisionManager playerCollisionManager)
    {
        player = playerCollisionManager;
    }

    private void FillTimersDictionary()
    {
        bonusTimersDictionary.Add(KitchenMiniGameItems.Bonus_DoubleCoins, bonusTimer_DoubleCoins);
        bonusTimersDictionary.Add(KitchenMiniGameItems.Bonus_Shield, bonusTimer_Shield);
        bonusTimersDictionary.Add(KitchenMiniGameItems.Bonus_CoinsMagnet, bonusTimer_CoinsMagnet);
    }
}
