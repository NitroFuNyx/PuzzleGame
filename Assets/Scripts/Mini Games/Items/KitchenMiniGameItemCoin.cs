using UnityEngine;

public class KitchenMiniGameItemCoin : KitchenMiniGameItem
{
    [Header("Item Data")]
    [Space]
    [SerializeField] private int coinsAmount = 1;

    public int CoinsAmount { get => coinsAmount; }

    public override void OnInteractionWithPlayer_ExecuteReaction(PlayerCollisionManager player)
    {
        if(player.CanCollectItems)
        {
            _poolItemsManager.ReturnItemToPool(poolItemComponent, itemType);
        }
    }
}
