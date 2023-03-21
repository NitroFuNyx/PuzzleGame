using UnityEngine;
using System.Collections;
using DG.Tweening;

public class KitchenMiniGameItemCoin : KitchenMiniGameItem
{
    [Header("Item Data")]
    [Space]
    [SerializeField] private int coinsAmount = 1;
    [Header("Move To Player Data")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float distanceToPlayerTreshold = 0.1f;

    private Coroutine rotationCoroutine;

    private bool coinsMagnetCollision = false;

    public int CoinsAmount { get => coinsAmount; }

    private void OnEnable()
    {
        coinsMagnetCollision = false;
        StartCoroutine(ResetPlayerInteractionVFXCoroutine());
        rotationCoroutine = StartCoroutine(RotateCoroutine());
    }
    [ContextMenu("Rotate")]
    public void Rotate()
    {
        StartCoroutine(RotateCoroutine());
    }

    public override void OnInteractionWithPlayer_ExecuteReaction(PlayerCollisionManager player)
    {
        if(player.CanCollectItems)
        {
            PlayItemInteractionVFX();
            StopCoroutine(rotationCoroutine);
            _poolItemsManager.ReturnItemToPool(poolItemComponent, itemType);
        }
    }

    public override void OnInteractionWithCoinsMagnet_ExecuteReaction(Collider2D collision)
    {
        if(!coinsMagnetCollision)
        {
            coinsMagnetCollision = true;
            StartCoroutine(MoveToPlayerCoroutine(collision.transform));
        }    
    }

    private IEnumerator MoveToPlayerCoroutine(Transform player)
    {
        Vector3 startPos = transform.position;
        float travelPercent = 0f;

        while(travelPercent < 1f)
        {
            travelPercent += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(startPos, player.position, travelPercent);
            yield return new WaitForEndOfFrame();
        }
    }
}
