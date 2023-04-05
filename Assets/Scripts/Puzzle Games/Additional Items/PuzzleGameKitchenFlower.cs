using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class PuzzleGameKitchenFlower : MonoBehaviour, Iinteractable
{
    [Header("Scale Data")]
    [Space]
    [SerializeField] private Vector3 scaleDelta = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField] private float scaleDuration = 1f;
    [SerializeField] private Ease scaleFunction;
    [SerializeField] private int scaleMaxCount = 4;
    [Header("Jump Data")]
    [Space]
    [SerializeField] private Vector3 jumpDeltaVector = new Vector3(2f, 0f, 0f);
    [SerializeField] private float jumpDuration = 1f;
    [SerializeField] private float jumpPower = 1f;
    [Header("Internal References")]
    [Space]
    [SerializeField] private PuzzleKey key;
    [SerializeField] private PuzzleKeyContainer keyContainerComponent;

    private bool animationInProcess = false;
    private bool containsKey = true;

    private int scaleCounter = 0;

    private void Start()
    {
        if (TryGetComponent(out PuzzleClueHolder clueHolder))
        {
            clueHolder.ClueIndex = key.KeyIndex;
        }
        if (key)
        {
            key.OnKeyCollected += KeyCollected_ExecuteReaction;
        }
        if (keyContainerComponent)
        {
            keyContainerComponent.OnCollectedItemsDataLoaded += CollectedItemsDataLoaded_ExecuteReaction;
        }
        StartCoroutine(SetStartSettingsCoroutine());
    }

    private void OnDestroy()
    {
        if (key)
        {
            key.OnKeyCollected -= KeyCollected_ExecuteReaction;
        }
        if (keyContainerComponent)
        {
            keyContainerComponent.OnCollectedItemsDataLoaded -= CollectedItemsDataLoaded_ExecuteReaction;
        }
    }

    public void Interact()
    {
        ScaleObject();
    }

    private void ScaleObject()
    {
        if(!animationInProcess && containsKey)
        {
            animationInProcess = true;
            scaleCounter++;
            Vector3 scaleVector = transform.localScale + scaleDelta;

            if (scaleCounter < scaleMaxCount)
            {
                transform.DOScale(scaleVector, scaleDuration).SetEase(scaleFunction).OnComplete(() =>
                {
                    animationInProcess = false;
                });
                
            }
            else
            {
                transform.DOScale(scaleVector, scaleDuration).SetEase(scaleFunction);
                StartCoroutine(JumpCoroutine(scaleDuration / 2f));
            }
        }
    }

    private void KeyCollected_ExecuteReaction()
    {
        containsKey = false;
    }

    private void CollectedItemsDataLoaded_ExecuteReaction(List<PuzzleGameKitchenItems> collectedItemsList, List<PuzzleGameKitchenItems> usedItemsList)
    {
        if (key != null && (collectedItemsList.Contains(key.Item) || usedItemsList.Contains(key.Item)))
        {
            containsKey = false;
        }
    }

    private IEnumerator JumpCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.DOJump(transform.position + jumpDeltaVector, jumpPower, 1, jumpDuration);
        key.gameObject.SetActive(true);
    }

    private IEnumerator SetStartSettingsCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        key.gameObject.SetActive(false);
    }
}
