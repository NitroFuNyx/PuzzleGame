using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

public class KitchenMiniGameBonusTimersPanel : MonoBehaviour
{
    [Header("Panel Timers")]
    [Space]
    [SerializeField] private KitchenMiniGameBonusTimer bonusTimer_DoubleCoins;
    [SerializeField] private KitchenMiniGameBonusTimer bonusTimer_Shield;
    [SerializeField] private KitchenMiniGameBonusTimer bonusTimer_CoinsMagnet;
    [SerializeField] private List<KitchenMiniGameBonusTimer> bonusTimersList = new List<KitchenMiniGameBonusTimer>();
    [Header("Timer Panels")]
    [Space]
    [SerializeField] private float hideTimerPanelDelay = 0.5f;
    [SerializeField] private float hideTimerPanelDuration = 0.5f;
    [SerializeField] private float checkTimersValueDelay = 0.1f;

    private Dictionary<KitchenMiniGameItems, KitchenMiniGameBonusTimer> bonusTimersDictionary = new Dictionary<KitchenMiniGameItems, KitchenMiniGameBonusTimer>();

    private PlayerCollisionManager player;

    private void Awake()
    {
        FillTimersDictionary();
    }

    public void UpdateBonusTimer(KitchenMiniGameItems itemType, float currentTime)
    {
        bonusTimersDictionary[itemType].UpdateTimer(currentTime);
        StartCoroutine(CheckTimersValueCoroutine());

        if(currentTime == 0f)
        {
            StartCoroutine(HideTimerPanelCoroutine(itemType));
        }
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

    private void CheckTimersValue()
    {
        bonusTimersList = bonusTimersList.OrderBy(timer => timer.TimerValue).ToList();

        for(int i = 0; i < bonusTimersList.Count; i++)
        {
            bonusTimersList[i].transform.SetSiblingIndex(i);
        }
    }

    private IEnumerator HideTimerPanelCoroutine(KitchenMiniGameItems itemType)
    {
        yield return new WaitForSeconds(hideTimerPanelDelay);
        bonusTimersDictionary[itemType].ActivationManager.HidePanelSlowly(hideTimerPanelDuration);
    }

    private IEnumerator CheckTimersValueCoroutine()
    {
        yield return new WaitForSeconds(checkTimersValueDelay);
        CheckTimersValue();
    }
}
