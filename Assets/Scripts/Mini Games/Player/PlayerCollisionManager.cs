using UnityEngine;
using Zenject;
using System.Collections;

public class PlayerCollisionManager : MonoBehaviour
{
    [Header("Debuffs Data")]
    [Space]
    [SerializeField] private float startDebuffTime = 2f;
    [SerializeField] private float currentDebuffTime = 0f;

    private BoxCollider2D boxCollider;

    private ResourcesManager _resourcesManager;

    private bool canCollectItems = false;

    public bool CanCollectItems { get => canCollectItems; private set => canCollectItems = value; }

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
    }

    private IEnumerator DebuffItemCollision_ExecuteReactionCoroutine()
    {
        currentDebuffTime = startDebuffTime;
        canCollectItems = false;

        while(currentDebuffTime > 0f)
        {
            yield return new WaitForSeconds(1f);
            currentDebuffTime--;
        }

        canCollectItems = true;
    }
}
