using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public abstract class KitchenMiniGameItem : MonoBehaviour
{
    [Header("Item Data")]
    [Space]
    [SerializeField] protected KitchenMiniGameItems itemType;
    [Header("Sprites")]
    [Space]
    [SerializeField] private List<Sprite> itemsSpritesList = new List<Sprite>();
    [Header("VFX")]
    [Space]
    [SerializeField] protected ParticleSystem playerStandartInteractionVFX;

    protected PoolItemsManager _poolItemsManager;

    protected SpriteRenderer spriteRenderer;
    protected PoolItem poolItemComponent;

    private Vector3 startPos;

    private float vfxResetDelay = 0.1f;

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
        startPos = transform.localPosition;
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
        else if (collision.gameObject.layer == Layers.KitchenMiniGamePlayerCoinsMagnetColliderLayer)
        {
            OnInteractionWithCoinsMagnet_ExecuteReaction(collision);
        }
    }

    protected void SetItemSprite()
    {
        int index = Random.Range(0, itemsSpritesList.Count);
        spriteRenderer.sprite = itemsSpritesList[index];
    }

    protected void PlayItemInteractionVFX()
    {
        playerStandartInteractionVFX.transform.SetParent(null);
        playerStandartInteractionVFX.Play();
    }

    public abstract void OnInteractionWithPlayer_ExecuteReaction(PlayerCollisionManager player);

    public abstract void OnInteractionWithCoinsMagnet_ExecuteReaction(Collider2D collision);

    protected IEnumerator ResetPlayerInteractionVFXCoroutine()
    {
        yield return new WaitForSeconds(vfxResetDelay);
        playerStandartInteractionVFX.transform.SetParent(transform);
        playerStandartInteractionVFX.transform.localPosition = startPos;
    }
}
