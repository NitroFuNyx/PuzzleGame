using UnityEngine;

public class KitchenMiniGameDebuffItem : KitchenMiniGameItem
{
    [Header("Debuff Data")]
    [Space]
    [SerializeField] private float debuffTime = 2f;
    [Header("VFX")]
    [Space]
    [SerializeField] private ParticleSystem playerStandartInteractionVFX;

    public override void OnInteractionWithPlayer_ExecuteReaction(PlayerCollisionManager player)
    {
        if(player.CanCollectItems)
        {
            playerStandartInteractionVFX.transform.SetParent(null);
            playerStandartInteractionVFX.Play();
            _poolItemsManager.ReturnItemToPool(poolItemComponent, itemType);
        }
    }
}
