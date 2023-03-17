using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class KitchenMiniGameItem : MonoBehaviour
{
    [Header("Item Data")]
    [Space]
    [SerializeField] protected KitchenMiniGameItems itemType;
    [Header("Sprites")]
    [Space]
    [SerializeField] private List<Sprite> coinsSpritesList = new List<Sprite>();

    private PoolItemsManager _poolItemsManager;

    protected SpriteRenderer spriteRenderer;
    private PoolItem poolItemComponent;

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
    }

    //#region Zenject
    //[Inject]
    //private void Construct(PoolItemsManager poolItemsManager)
    //{
    //    _poolItemsManager = poolItemsManager;
    //}
    //#endregion Zenject

    protected void SetItemSprite()
    {
        int index = Random.Range(0, coinsSpritesList.Count);
        spriteRenderer.sprite = coinsSpritesList[index];
    }

    public abstract void OnInteractionWithPlayer_ExecuteReaction(); 
}
