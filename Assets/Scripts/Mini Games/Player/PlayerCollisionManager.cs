using UnityEngine;
using System;
using Zenject;
using System.Collections;
using TMPro;

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
    [Header("VFX")]
    [Space]
    [SerializeField] private ParticleSystem stunVFX;

    private BoxCollider2D boxCollider;

    private ResourcesManager _resourcesManager;

    private bool canCollectItems = false;
    private bool doubleCoinsBuffActivated = false;
    private bool shieldBonusActivated = false;

    public bool CanCollectItems { get => canCollectItems; private set => canCollectItems = value; }

    #region Events Declaration
    public event Action OnCharacterStunned;
    public event Action OnCharacterStunnedStateFinished;

    public event Action<float> OnAdditionalTimeBonusCollected;
    #endregion Events Declaration

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
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
    private void Construct(ResourcesManager resourcesManager)
    {
        _resourcesManager = resourcesManager;
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
                StartCoroutine(DoubleCoinsBonusCollision_ExecuteReactionCoroutine());
            }
            else
            {
                currentDoubleCoinsBonusTime += startDoubleCoinsBonusTime;
                Debug.Log($"Surplus");
            }
        }
        else if (collision.gameObject.TryGetComponent(out KitchenMiniGameItemBonus_Shield itemBonus_ShieldBonus))
        {
            if (!shieldBonusActivated)
            {
                shieldBonusActivated = true;
                StartCoroutine(ShieldBonusCollision_ExecuteReactionCoroutine());
            }
            else
            {
                currentShieldBonusTime += startShieldBonusTime;
                Debug.Log($"Surplus");
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
        currentDoubleCoinsBonusTime = startDoubleCoinsBonusTime;

        while(currentDoubleCoinsBonusTime > 0f)
        {
            yield return new WaitForSeconds(1f);
            currentDoubleCoinsBonusTime--;
        }

        doubleCoinsBuffActivated = false;
    }

    private IEnumerator ShieldBonusCollision_ExecuteReactionCoroutine()
    {
        currentShieldBonusTime = startShieldBonusTime;

        while (currentShieldBonusTime > 0f)
        {
            yield return new WaitForSeconds(1f);
            currentShieldBonusTime--;
        }

        shieldBonusActivated = false;
    }
}
