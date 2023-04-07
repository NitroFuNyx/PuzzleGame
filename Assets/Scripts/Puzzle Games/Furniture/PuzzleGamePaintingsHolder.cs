using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGamePaintingsHolder : MonoBehaviour
{
    [Header("Internal References")]
    [Space]
    [SerializeField] private PuzzleKey key;
    [SerializeField] private PuzzleKeyContainer keyContainerComponent;
    [Header("Paintings")]
    [Space]
    [SerializeField] private PuzzleGamePainting leftPainting;
    [SerializeField] private PuzzleGamePainting middlePainting;

    private bool containsKey = true;

    private void Awake()
    {
        leftPainting.CashComponents(this);
        middlePainting.CashComponents(this);
    }

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

    public void PaintingRotated_ExecuteReaction()
    {
        if(containsKey)
        {
            if(leftPainting.IsInCorrectPos && middlePainting.IsInCorrectPos)
            {
                key.gameObject.SetActive(true);
                key.ChangeKeySimulattionState(true);
            }
        }
    }

    private void KeyCollected_ExecuteReaction()
    {
        containsKey = false;
        key.ChangeKeySimulattionState(false);
    }

    private void CollectedItemsDataLoaded_ExecuteReaction(List<PuzzleGameKitchenItems> collectedItemsList, List<PuzzleGameKitchenItems> usedItemsList)
    {
        if (key != null && (collectedItemsList.Contains(key.Item) || usedItemsList.Contains(key.Item)))
        {
            containsKey = false;
            key.ChangeKeySimulattionState(false);
        }
    }

    private IEnumerator SetStartSettingsCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        if (key)
            key.gameObject.SetActive(false);
    }
}
