using UnityEngine;
using System.Collections;

public class KitchenMiniGameItemCoin : KitchenMiniGameItem
{
    [Header("Item Data")]
    [Space]
    [SerializeField] private int coinsAmount = 1;

    public int CoinsAmount { get => coinsAmount; }

    private void OnEnable()
    {
        StartCoroutine(ResetPlayerInteractionVFXCoroutine());
    }

    public override void OnInteractionWithPlayer_ExecuteReaction(PlayerCollisionManager player)
    {
        if(player.CanCollectItems)
        {
            PlayItemInteractionVFX();
            _poolItemsManager.ReturnItemToPool(poolItemComponent, itemType);
        }
    }
}
