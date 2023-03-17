using UnityEngine;

public class KitchenMiniGameItemCoin : KitchenMiniGameItem
{
    [Header("Item Data")]
    [Space]
    [SerializeField] private int coinsAmount = 1;
    [Header("VFX")]
    [Space]
    [SerializeField] private ParticleSystem playerStandartInteractionVFX; 

    public int CoinsAmount { get => coinsAmount; }

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
