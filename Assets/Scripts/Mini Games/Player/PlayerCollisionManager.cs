using UnityEngine;
using System;
using Zenject;
using System.Collections;

public class PlayerCollisionManager : MonoBehaviour
{
    [Header("Debuffs Data")]
    [Space]
    [SerializeField] private float startDebuffTime = 2f;
    [SerializeField] private float currentDebuffTime = 0f;
    [Header("Double Coins Bonus Data")]
    [Space]
    [SerializeField] private float startDoubleCoinsBonusTime = 5f;
    [SerializeField] private float currentDoubleCoinsBonusTime = 0f;
    [Header("Shield Bonus Data")]
    [Space]
    [SerializeField] private float startShieldBonusTime = 5f;
    [SerializeField] private float currentShieldBonusTime = 0f;
    [Header("Coins Magnet Bonus Data")]
    [Space]
    [SerializeField] private float startCoinsMagnetBonusTime = 5f;
    [SerializeField] private float currentCoinsMagnetBonusTime = 0f;
    [Header("Collider Objects")]
    [Space]
    [SerializeField] private GameObject coinsMagnetColliderObject;
    [Header("VFX")]
    [Space]
    [SerializeField] private ParticleSystem stunVFX;

    private BoxCollider2D boxCollider;
    private CircleCollider2D coinsMagnetCollider;

    private ResourcesManager _resourcesManager;
    private KitchenMiniGameBonusTimersPanel _kitchenMiniGameBonusTimersPanel;

    private bool canCollectItems = false;

    private bool doubleCoinsBuffActivated = false;
    private bool shieldBonusActivated = false;
    private bool coinsMagnetBonusActivated = false;

    public bool CanCollectItems { get => canCollectItems; private set => canCollectItems = value; }

    public float CurrentDoubleCoinsBonusTime { get => currentDoubleCoinsBonusTime; private set => currentDoubleCoinsBonusTime = value; }
    public float CurrentShieldBonusTime { get => currentShieldBonusTime; private set => currentShieldBonusTime = value; }
    public float CurrentCoinsMagnetBonusTime { get => currentCoinsMagnetBonusTime; private set => currentCoinsMagnetBonusTime = value; }

    #region Events Declaration
    public event Action OnCharacterStunned;
    public event Action OnCharacterStunnedStateFinished;

    public event Action<float> OnAdditionalTimeBonusCollected;
    #endregion Events Declaration

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        coinsMagnetCollider = coinsMagnetColliderObject.GetComponent<CircleCollider2D>();
        coinsMagnetCollider.enabled = false;
    }

    private void Start()
    {
        _kitchenMiniGameBonusTimersPanel.CashComponents(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canCollectItems)
        {
            CheckCollisionItems(collision);
        }
    }

    #region Zenject
    [Inject]
    private void Construct(ResourcesManager resourcesManager, KitchenMiniGameBonusTimersPanel kitchenMiniGameBonusTimersPanel)
    {
        _resourcesManager = resourcesManager;
        _kitchenMiniGameBonusTimersPanel = kitchenMiniGameBonusTimersPanel;
    }
    #endregion Zenject

    public void ChangeState_CanCollectItems(bool canCollect)
    {
        canCollectItems = canCollect;
    }

    private void CheckCollisionItems(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out KitchenMiniGameItemCoin item_Coin))
        {
            if(!doubleCoinsBuffActivated)
            {
                _resourcesManager.IncreaseCurrentLevelCoins(item_Coin.CoinsAmount);
            }
            else
            {
                _resourcesManager.IncreaseCurrentLevelCoins(item_Coin.CoinsAmount * 2);
            }
        }
        else if(collision.gameObject.TryGetComponent(out KitchenMiniGameDebuffItem item_Debuff))
        {
            if(!shieldBonusActivated)
            {
                StartCoroutine(DebuffItemCollision_ExecuteReactionCoroutine());
            }
        }
        else if (collision.gameObject.TryGetComponent(out KitchenMiniGameItemBonus_AdditionalTime itemBonus_AdditionalCoins))
        {
            OnAdditionalTimeBonusCollected?.Invoke(itemBonus_AdditionalCoins.BonusTime);
        }
        else if (collision.gameObject.TryGetComponent(out KitchenMiniGameItemBonus_DoubleCoins itemBonus_DoubleCoins))
        {
            if(!doubleCoinsBuffActivated)
            {
                doubleCoinsBuffActivated = true;
                currentDoubleCoinsBonusTime = startDoubleCoinsBonusTime;
                StartCoroutine(DoubleCoinsBonusCollision_ExecuteReactionCoroutine());
            }
            else
            {
                currentDoubleCoinsBonusTime += startDoubleCoinsBonusTime;
                _kitchenMiniGameBonusTimersPanel.UpdateBonusTimer(KitchenMiniGameItems.Bonus_DoubleCoins, currentDoubleCoinsBonusTime);
            }
        }
        else if (collision.gameObject.TryGetComponent(out KitchenMiniGameItemBonus_Shield itemBonus_ShieldBonus))
        {
            if (!shieldBonusActivated)
            {
                shieldBonusActivated = true;
                currentShieldBonusTime = startShieldBonusTime;
                StartCoroutine(ShieldBonusCollision_ExecuteReactionCoroutine());
            }
            else
            {
                currentShieldBonusTime += startShieldBonusTime;
                _kitchenMiniGameBonusTimersPanel.UpdateBonusTimer(KitchenMiniGameItems.Bonus_Shield, currentShieldBonusTime);
            }
        }
        else if (collision.gameObject.TryGetComponent(out KitchenMiniGameItemBonus_CoinsMagnet itemBonus_MagnetBonus))
        {
            if (!coinsMagnetBonusActivated)
            {
                coinsMagnetBonusActivated = true;
                coinsMagnetCollider.enabled = true;
                currentCoinsMagnetBonusTime = startCoinsMagnetBonusTime;
                StartCoroutine(CoinsMagnetBonusCollision_ExecuteReactionCoroutine());
            }
            else
            {
                currentCoinsMagnetBonusTime += startCoinsMagnetBonusTime;
                _kitchenMiniGameBonusTimersPanel.UpdateBonusTimer(KitchenMiniGameItems.Bonus_CoinsMagnet, CurrentCoinsMagnetBonusTime);
            }
        }
    }

    private IEnumerator DebuffItemCollision_ExecuteReactionCoroutine()
    {
        currentDebuffTime = startDebuffTime;
        canCollectItems = false;
        OnCharacterStunned?.Invoke();
        stunVFX.Play();

        while (currentDebuffTime > 0f)
        {
            yield return new WaitForSeconds(1f);
            currentDebuffTime--;
        }

        stunVFX.Stop();
        canCollectItems = true;
        OnCharacterStunnedStateFinished?.Invoke();
    }

    private IEnumerator DoubleCoinsBonusCollision_ExecuteReactionCoroutine()
    {
        while(currentDoubleCoinsBonusTime > 0f)
        {
            _kitchenMiniGameBonusTimersPanel.UpdateBonusTimer(KitchenMiniGameItems.Bonus_DoubleCoins, currentDoubleCoinsBonusTime);
            yield return new WaitForSeconds(1f);
            currentDoubleCoinsBonusTime--;
        }
        _kitchenMiniGameBonusTimersPanel.UpdateBonusTimer(KitchenMiniGameItems.Bonus_DoubleCoins, currentDoubleCoinsBonusTime);
        doubleCoinsBuffActivated = false;
    }

    private IEnumerator ShieldBonusCollision_ExecuteReactionCoroutine()
    {
        while (currentShieldBonusTime > 0f)
        {
            _kitchenMiniGameBonusTimersPanel.UpdateBonusTimer(KitchenMiniGameItems.Bonus_Shield, currentShieldBonusTime);
            yield return new WaitForSeconds(1f);
            currentShieldBonusTime--;
        }
        _kitchenMiniGameBonusTimersPanel.UpdateBonusTimer(KitchenMiniGameItems.Bonus_Shield, currentShieldBonusTime);
        shieldBonusActivated = false;
    }

    private IEnumerator CoinsMagnetBonusCollision_ExecuteReactionCoroutine()
    {
        while (currentCoinsMagnetBonusTime > 0f)
        {
            _kitchenMiniGameBonusTimersPanel.UpdateBonusTimer(KitchenMiniGameItems.Bonus_CoinsMagnet, CurrentCoinsMagnetBonusTime);
            yield return new WaitForSeconds(1f);
            currentCoinsMagnetBonusTime--;
        }
        _kitchenMiniGameBonusTimersPanel.UpdateBonusTimer(KitchenMiniGameItems.Bonus_CoinsMagnet, CurrentCoinsMagnetBonusTime);
        coinsMagnetBonusActivated = false;
        coinsMagnetCollider.enabled = false;
    }
}
