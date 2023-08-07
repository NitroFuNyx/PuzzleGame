using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class BushWithKeyInteractionHandler_Backyard : MonoBehaviour, Iinteractable
{
    [Header("Scale Data")]
    [Space]
    [SerializeField] private Vector3 scaleVector = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField] private float scaleDuration = 1f;
    [SerializeField] private int scaleFreequency = 4;
    [SerializeField] private Ease scaleFunction;
    [SerializeField] private int interactionMaxCount = 4;
    [Header("Internal References")]
    [Space]
    [SerializeField] private PuzzleKey key;
    [SerializeField] private PuzzleKeyContainer keyContainerComponent;

    private Vector3 startPos = new Vector3();
    private Vector3 startScale = new Vector3();

    private bool animationInProcess = false;
    private bool containsKey = true;
    private bool canInteract = true;

    private int interactionCounter = 0;

    private void Start()
    {
        //if (TryGetComponent(out PuzzleClueHolder clueHolder))
        //{
        //    clueHolder.ClueIndex = key.KeyIndex;
        //}
        //if (key)
        //{
        //    key.OnKeyCollected += KeyCollected_ExecuteReaction;
        //}
        //if (keyContainerComponent)
        //{
        //    keyContainerComponent.OnCollectedItemsDataLoaded += CollectedItemsDataLoaded_ExecuteReaction;
        //    keyContainerComponent.OnKeyDataReset += ResetKeyData_ExecuteReaction;
        //}

        //startPos = transform.position;
        //startScale = transform.localScale;

        //StartCoroutine(SetStartSettingsCoroutine());
    }

    private void OnDestroy()
    {
        //if (key)
        //{
        //    key.OnKeyCollected -= KeyCollected_ExecuteReaction;
        //}
        //if (keyContainerComponent)
        //{
        //    keyContainerComponent.OnCollectedItemsDataLoaded -= CollectedItemsDataLoaded_ExecuteReaction;
        //    keyContainerComponent.OnKeyDataReset -= ResetKeyData_ExecuteReaction;
        //}
    }

    public void Interact()
    {
        ScaleObject();
    }

    public void ResetBush()
    {
        transform.localScale = startScale;
        transform.position = startPos;
        animationInProcess = false;
        interactionCounter = 0;
        canInteract = true;
    }

    private void ScaleObject()
    {
        if (containsKey)
        {
            animationInProcess = true;
            interactionCounter++;

            if (interactionCounter < interactionMaxCount)
            {
                if (canInteract)
                {
                    transform.DOPunchScale(scaleVector, scaleDuration, scaleFreequency).SetEase(scaleFunction).OnComplete(() =>
                    {
                        animationInProcess = false;
                    });
                }

            }
            else
            {
                if (canInteract)
                {
                    canInteract = false;
                    transform.DOKill();
                    // give key
                }
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

    public void ResetKeyData_ExecuteReaction()
    {
        containsKey = true;
    }

    private IEnumerator SetStartSettingsCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        key.gameObject.SetActive(false);
    }
}
