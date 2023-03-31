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

    private bool isOpen = false;

    private void Awake()
    {
        StartCoroutine(SetStartSettingsCoroutine());
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
        }
        else
        {
            if(containsKey)
            {
                key.gameObject.SetActive(true);
            }
            if(doorAdditionalItem)
            {
                doorAdditionalItem.SetActive(false);
            }
            door.sprite = openDoorSprite;
        }
    }

    public override void KeyCollected_ExecuteReaction()
    {
        containsKey = false;
    }

    public override void CollectedItemsDataLoaded_ExecuteReaction(List<PuzzleGameKitchenItems> collectedItemsList)
    {
        if(key != null && collectedItemsList.Contains(key.Item))
        {
            containsKey = false;
        }
    }

    private IEnumerator SetStartSettingsCoroutine()
    {
        yield return new WaitForSeconds(1f);
        if (key != null)
        {
            key.gameObject.SetActive(false);
        }
    }
}
