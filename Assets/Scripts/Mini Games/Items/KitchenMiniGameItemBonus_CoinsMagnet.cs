using UnityEngine;

public class KitchenMiniGameItemBonus_CoinsMagnet : KitchenMiniGameItem
{
    [Header("Idle VFX")]
    [Space]
    [SerializeField] private ParticleSystem idleBonusVFX;

    private Coroutine rotationCoroutine;

    private void OnEnable()
    {
        idleBonusVFX.Play();
        StartCoroutine(ResetPlayerInteractionVFXCoroutine());
        rotationCoroutine = StartCoroutine(RotateCoroutine());
    }

    public override void OnInteractionWithPlayer_ExecuteReaction(PlayerCollisionManager player)
    {
        if (player.CanCollectItems)
        {
            PlayItemInteractionVFX();
            StopCoroutine(rotationCoroutine);
            _poolItemsManager.ReturnItemToPool(poolItemComponent, itemType);
        }
    }

    public override void OnInteractionWithCoinsMagnet_ExecuteReaction(Collider2D collision)
    {
        
    }
}
