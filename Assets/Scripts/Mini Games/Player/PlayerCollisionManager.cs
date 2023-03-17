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
    [Header("VFX")]
    [Space]
    [SerializeField] private ParticleSystem stunVFX;

    private BoxCollider2D boxCollider;

    private ResourcesManager _resourcesManager;

    private bool canCollectItems = false;

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
            _resourcesManager.IncreaseCurrentLevelCoins(item_Coin.CoinsAmount);
        }
        else if(collision.gameObject.TryGetComponent(out KitchenMiniGameDebuffItem item_Debuff))
        {
            StartCoroutine(DebuffItemCollision_ExecuteReactionCoroutine());
        }
        else if (collision.gameObject.TryGetComponent(out KitchenMiniGameItemBonus_AdditionalTime item_Bonus))
        {
            OnAdditionalTimeBonusCollected?.Invoke(item_Bonus.BonusTime);
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
}
