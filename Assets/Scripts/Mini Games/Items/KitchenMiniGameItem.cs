using System.Collections.Generic;
using UnityEngine;

public abstract class KitchenMiniGameItem : MonoBehaviour
{
    [Header("Item Data")]
    [Space]
    [SerializeField] protected KitchenMiniGameItems itemType;
    [Header("Sprites")]
    [Space]
    [SerializeField] private List<Sprite> coinsSpritesList = new List<Sprite>();

    protected PoolItemsManager _poolItemsManager;

    protected SpriteRenderer spriteRenderer;
    protected PoolItem poolItemComponent;

    public KitchenMiniGameItems ItemType { get => itemType; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        poolItemComponent = GetComponent<PoolItem>();
        _poolItemsManager = FindObjectOfType<PoolItemsManager>();
    }

    private void Start()
    {
        SetItemSprite();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Layers.KitchenMiniGameBottomBorderLayer)
        {
            _poolItemsManager.ReturnItemToPool(poolItemComponent, itemType);
        }
        else if(collision.TryGetComponent(out PlayerCollisionManager player))
        {
            OnInteractionWithPlayer_ExecuteReaction(player);
        }
    }

    protected void SetItemSprite()
    {
        int index = Random.Range(0, coinsSpritesList.Count);
        spriteRenderer.sprite = coinsSpritesList[index];
    }

    public abstract void OnInteractionWithPlayer_ExecuteReaction(PlayerCollisionManager player); 
}
