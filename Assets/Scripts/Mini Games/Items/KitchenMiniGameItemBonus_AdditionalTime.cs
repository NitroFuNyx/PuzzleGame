using UnityEngine;

public class KitchenMiniGameItemBonus_AdditionalTime : KitchenMiniGameItem
{
    [Header("Idle VFX")]
    [Space]
    [SerializeField] private ParticleSystem idleBonusVFX;

    public override void OnInteractionWithPlayer_ExecuteReaction(PlayerCollisionManager player)
    {
        if (player.CanCollectItems)
        {
            PlayItemInteractionVFX();
            _poolItemsManager.ReturnItemToPool(poolItemComponent, itemType);
            //StartCoroutine(ResetVFXCoroutine());
        }
    }
}
