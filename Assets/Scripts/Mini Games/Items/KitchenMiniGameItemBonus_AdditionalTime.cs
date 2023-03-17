using UnityEngine;

public class KitchenMiniGameItemBonus_AdditionalTime : KitchenMiniGameItem
{
    [Header("Idle VFX")]
    [Space]
    [SerializeField] private ParticleSystem idleBonusVFX;

    private void OnEnable()
    {
        idleBonusVFX.Play();
        StartCoroutine(ResetPlayerInteractionVFXCoroutine());
    }

    private void Start()
    {
        idleBonusVFX.Play();
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
