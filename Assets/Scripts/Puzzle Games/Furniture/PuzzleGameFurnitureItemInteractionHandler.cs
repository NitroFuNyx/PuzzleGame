using UnityEngine;
using System.Collections.Generic;

public abstract class PuzzleGameFurnitureItemInteractionHandler : MonoBehaviour, Iinteractable
{
    [Header("Item Data")]
    [Space]
    [SerializeField] protected bool containsKey = false;
    [Header("Keys")]
    [Space]
    [SerializeField] protected PuzzleKey key;
    [Header("Internal References")]
    [Space]
    [SerializeField] protected PuzzleKeyContainer keyContainerComponent;
    
    private void Start()
    {
        if (key)
        {
            key.OnKeyCollected += KeyCollected_ExecuteReaction;
        }
        if (keyContainerComponent)
        {
            keyContainerComponent.OnCollectedItemsDataLoaded += CollectedItemsDataLoaded_ExecuteReaction;
        }
    }

    private void OnDestroy()
    {
        if(key)
        {
            key.OnKeyCollected -= KeyCollected_ExecuteReaction;
        }
        if(keyContainerComponent)
        {
            keyContainerComponent.OnCollectedItemsDataLoaded -= CollectedItemsDataLoaded_ExecuteReaction;
        }
    }

    public void Interact()
    {
        InteractOnTouch();
    }

    public abstract void KeyCollected_ExecuteReaction();

    public abstract void CollectedItemsDataLoaded_ExecuteReaction(List<PuzzleGameKitchenItems> collectedItemsList, List<PuzzleGameKitchenItems> usedItemsList);

    public abstract void InteractOnTouch();
}
