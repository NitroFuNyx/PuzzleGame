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
    [SerializeField] private PuzzleButton miniGameButton;
    [SerializeField] private PuzzleGameItem_MiniGameModeStarter miniGameModeStarter;
    [SerializeField] private BoxCollider2D straw;

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
            keyContainerComponent.OnKeyDataReset += ResetKeyData_ExecuteReaction;
        }
        if (miniGameButton)
        {
            miniGameButton.SetButtonActivation(false);
        }
        if(straw != null)
        {
            straw.enabled = false;
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
            keyContainerComponent.OnKeyDataReset -= ResetKeyData_ExecuteReaction;
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
                //miniGameButton.gameObject.SetActive(false);
                miniGameButton.SetButtonActivation(false);
            }
            if (miniGameModeStarter)
            {
                miniGameModeStarter.ChangeItemActivation(false);
            }
            if(straw)
            {
                straw.enabled = false;
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
            if(miniGameButton)
            {
                //miniGameButton.gameObject.SetActive(true);
                miniGameButton.SetButtonActivation(true);
            }
            if (miniGameModeStarter)
            {
                miniGameModeStarter.ChangeItemActivation(true);
            }
            if(straw)
            {
                straw.enabled = true;
            }
            door.sprite = openDoorSprite;
        }
    }

    public override void KeyCollected_ExecuteReaction()
    {
        containsKey = false;
        if (miniGameModeStarter && containsKey)
        {
            miniGameModeStarter.ChangeItemActivation(false);
        }
    }

    public override void CollectedItemsDataLoaded_ExecuteReaction(List<PuzzleGameCollectableItems> collectedItemsList, List<PuzzleGameCollectableItems> usedItemsList)
    {
        if(key != null && (collectedItemsList.Contains(key.Item) || usedItemsList.Contains(key.Item)))
        {
            containsKey = false;
            if (miniGameModeStarter && containsKey)
            {
                miniGameModeStarter.ChangeItemActivation(false);
            }
        }
    }

    public void ResetKeyData_ExecuteReaction()
    {
        containsKey = true;
    }

    public void ResetItem()
    {
        isOpen = false;
        if (!isOpen)
        {
            door.sprite = closedDoorSprite;
            if (doorAdditionalItem)
            {
                doorAdditionalItem.SetActive(true);
            }
            if (key)
            {
                key.gameObject.SetActive(false);
            }
            if (miniGameButton)
            {
                miniGameButton.SetButtonActivation(false);
            }
            if (miniGameModeStarter)
            {
                miniGameModeStarter.ChangeItemActivation(false);
            }
        }
        else
        {
            if (containsKey)
            {
                key.gameObject.SetActive(true);
            }
            if (doorAdditionalItem)
            {
                doorAdditionalItem.SetActive(false);
            }
            if (miniGameButton)
            {
                miniGameButton.SetButtonActivation(true);
            }
            if (miniGameModeStarter)
            {
                miniGameModeStarter.ChangeItemActivation(true);
            }
            door.sprite = openDoorSprite;
        }
    }

    private IEnumerator SetStartSettingsCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        if (key != null)
        {
            key.gameObject.SetActive(false);
        }
        if (miniGameModeStarter)
        {
            miniGameModeStarter.ChangeItemActivation(false);
        }
    }
}