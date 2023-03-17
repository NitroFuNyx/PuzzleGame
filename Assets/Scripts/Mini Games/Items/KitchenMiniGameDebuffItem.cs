using UnityEngine;

public class KitchenMiniGameDebuffItem : KitchenMiniGameItem
{
    [Header("Debuff Data")]
    [Space]
    [SerializeField] private float debuffTime = 2f;

    public override void OnInteractionWithPlayer_ExecuteReaction(PlayerCollisionManager player)
    {
        if(player.CanCollectItems)
        {
            _poolItemsManager.ReturnItemToPool(poolItemComponent, itemType);
        }
    }
}
