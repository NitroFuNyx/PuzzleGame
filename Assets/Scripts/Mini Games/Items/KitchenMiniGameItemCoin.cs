using UnityEngine;
using System.Collections;

public class KitchenMiniGameItemCoin : KitchenMiniGameItem
{
    [Header("Item Data")]
    [Space]
    [SerializeField] private int coinsAmount = 1;
    [Header("VFX")]
    [Space]
    [SerializeField] private ParticleSystem playerStandartInteractionVFX;
    [Header("Delays")]
    [Space]
    [SerializeField] private float vfxResetDelay = 2f;

    private Vector3 startPos;

    public int CoinsAmount { get => coinsAmount; }

    private void Start()
    {
        startPos = transform.localPosition;
    }

    public override void OnInteractionWithPlayer_ExecuteReaction(PlayerCollisionManager player)
    {
        if(player.CanCollectItems)
        {
            playerStandartInteractionVFX.transform.SetParent(null);
            playerStandartInteractionVFX.Play();
            _poolItemsManager.ReturnItemToPool(poolItemComponent, itemType);
            //StartCoroutine(ResetVFXCoroutine());
        }
    }

    private IEnumerator ResetVFXCoroutine()
    {
        yield return new WaitForSeconds(vfxResetDelay);
        playerStandartInteractionVFX.transform.SetParent(transform);
        playerStandartInteractionVFX.transform.localPosition = startPos;
    }
}
