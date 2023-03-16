using UnityEngine;

public class KitchenMiniGameItemCoin : KitchenMiniGameItem
{
    [Header("Item Data")]
    [Space]
    [SerializeField] private int coinsAmount = 1;

    public override void OnInteractionWithPlayer_ExecuteReaction()
    {
        
    }
}
