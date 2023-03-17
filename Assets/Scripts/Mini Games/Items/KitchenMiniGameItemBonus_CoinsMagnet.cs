using UnityEngine;

public class KitchenMiniGameItemBonus_CoinsMagnet : KitchenMiniGameItem
{
    [Header("Idle VFX")]
    [Space]
    [SerializeField] private ParticleSystem idleBonusVFX;

    private void OnEnable()
    {
        idleBonusVFX.Play();
        StartCoroutine(ResetPlayerInteractionVFXCoroutine());
    }

    public override void OnInteractionWithPlayer_ExecuteReaction(PlayerCollisionManager player)
    {
        if (player.CanCollectItems)
        {
            PlayItemInteractionVFX();
            _poolItemsManager.ReturnItemToPool(poolItemComponent, itemType);
        }
    }
}
