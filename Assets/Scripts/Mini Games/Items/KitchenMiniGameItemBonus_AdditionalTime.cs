using UnityEngine;

public class KitchenMiniGameItemBonus_AdditionalTime : KitchenMiniGameItem
{
    [Header("Idle VFX")]
    [Space]
    [SerializeField] private ParticleSystem idleBonusVFX;
    [Header("Bonus Data")]
    [Space]
    [SerializeField] private float bonusTime = 5f;

    private Coroutine rotationCoroutine;

    public float BonusTime { get => bonusTime; }

    private void OnEnable()
    {
        idleBonusVFX.Play();
        StartCoroutine(ResetPlayerInteractionVFXCoroutine());
        rotationCoroutine = StartCoroutine(RotateCoroutine());
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
            StopCoroutine(rotationCoroutine);
            _poolItemsManager.ReturnItemToPool(poolItemComponent, itemType);
        }
    }

    public override void OnInteractionWithCoinsMagnet_ExecuteReaction(Collider2D collision)
    {
    }
}
