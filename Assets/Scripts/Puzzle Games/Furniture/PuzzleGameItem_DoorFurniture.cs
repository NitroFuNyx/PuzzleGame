using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGameItem_DoorFurniture : PuzzleGameFurnitureItemInteractionHandler
{
    [Header("Door Data")]
    [Space]
    [SerializeField] private SpriteRenderer door;
    [Header("Sprites")]
    [Space]
    [SerializeField] private Sprite closedDoorSprite;
    [SerializeField] private Sprite openDoorSprite;
    [Header("Internal References")]
    [Space]
    [SerializeField] private GameObject doorAdditionalItem;
    [SerializeField] private GameObject miniGameButton;

    private bool isOpen = false;

    private void Awake()
    {
        StartCoroutine(SetStartSettingsCoroutine());
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
        if(miniGameButton)
        {
            miniGameButton.gameObject.SetActive(false);
        }
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

    public override void InteractOnTouch()
    {
        isOpen = !isOpen;
        
        if(!isOpen)
        {
            door.sprite = closedDoorSprite;
            if (doorAdditionalItem)
            {
                doorAdditionalItem.SetActive(true);
            }
            if(key)
            {
                key.gameObject.SetActive(false);
            }
            if(miniGameButton)
            {
                miniGameButton.gameObject.SetActive(false);
            }
        }
        else
        {
            if(containsKey)
            {
                //containsKey = false;
                key.gameObject.SetActive(true);
            }
            if(doorAdditionalItem)
            {
                doorAdditionalItem.SetActive(false);
            }
            if(miniGameButton)
            {
                miniGameButton.gameObject.SetActive(true);
            }
            door.sprite = openDoorSprite;
        }
    }

    public override void KeyCollected_ExecuteReaction()
    {
        containsKey = false;
    }

    public override void CollectedItemsDataLoaded_ExecuteReaction(List<PuzzleGameKitchenItems> collectedItemsList, List<PuzzleGameKitchenItems> usedItemsList)
    {
        if(key != null && (collectedItemsList.Contains(key.Item) || usedItemsList.Contains(key.Item)))
        {
            containsKey = false;
        }
    }

    private IEnumerator SetStartSettingsCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        if (key != null)
        {
            key.gameObject.SetActive(false);
        }
    }
}
