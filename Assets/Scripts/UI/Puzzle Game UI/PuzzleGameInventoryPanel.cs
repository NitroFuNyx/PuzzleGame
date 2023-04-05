using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PuzzleGameInventoryPanel : MonoBehaviour
{
    [Header("Inventory Items Cells")]
    [Space]
    [SerializeField] private List<InventoryPanelItemCell> inventoryCellsList = new List<InventoryPanelItemCell>();
    [Header("Internal References")]
    [Space]
    [SerializeField] private Transform inventoryGrid;
    [SerializeField] private ScrollRect scrollRect;
    [Header("Prefabs")]
    [Space]
    [SerializeField] private InventoryPanelItemCell invenoryCellPrefab;
    [Header("Sprites")]
    [Space]
    [SerializeField] private List<Sprite> collectableItemsSpritesList = new List<Sprite>();
    [Header("Buttons")]
    [Space]
    [SerializeField] private InventoryScrollButton rightScrollButton;
    [SerializeField] private InventoryScrollButton leftScrollButton;
    [Header("Scroll Data")]
    [Space]
    [SerializeField] private float scrollDelta = 100f;

    private InventoryPanelItemCell currentlySelectedInventoryCell;
    private PuzzleGamesEnvironmentsHolder _puzzleGamesEnvironmentsHolder;

    private int inventoryCellsTreshold = 4;

    public InventoryPanelItemCell CurrentlySelectedInventoryCell { get => currentlySelectedInventoryCell; private set => currentlySelectedInventoryCell = value; }

    private void Awake()
    {
        ChangeScrollButtonsState(false);
    }

    private void Start()
    {
        SubscribeOnEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    #region Zenject
    [Inject]
    private void Construct(PuzzleGamesEnvironmentsHolder puzzleGamesEnvironmentsHolder)
    {
        _puzzleGamesEnvironmentsHolder = puzzleGamesEnvironmentsHolder;
    }
    #endregion Zenject

    public void PutItemInInventoryCell(Sprite sprite, PuzzleGameKitchenItems item)
    {
        var cell = Instantiate(invenoryCellPrefab, Vector3.zero, Quaternion.identity, inventoryGrid);
        cell.SetItemData(sprite, item);
        inventoryCellsList.Add(cell);
        cell.CashComponents(this);

        if (inventoryCellsList.Count > inventoryCellsTreshold)
        {
            ChangeScrollButtonsState(true);
        }
        else
        {
            ChangeScrollButtonsState(false);
        }
    }

    public void LoadCollectedItems(List<int> collectedItemsList)
    {
        for(int i = 0; i < collectedItemsList.Count; i++)
        {
            Sprite itemSprite = collectableItemsSpritesList[collectedItemsList[i]];

            PutItemInInventoryCell(itemSprite, (PuzzleGameKitchenItems)collectedItemsList[i]);
        }

        if (collectedItemsList.Count > inventoryCellsTreshold)
        {
            ChangeScrollButtonsState(true);
        }
        else
        {
            ChangeScrollButtonsState(false);
        }
    }

    public void InventoryItemButtonPressed(InventoryPanelItemCell cell)
    {
        for(int i = 0; i < inventoryCellsList.Count; i++)
        {
            if(inventoryCellsList[i] != cell)
            {
                inventoryCellsList[i].ResetSelectedState();
            }
        }

        currentlySelectedInventoryCell = cell;
    }

    public void InventoryItemButtonSelectionReset()
    {
        currentlySelectedInventoryCell = null;
    }

    public void LockKeyMismatched_ExecuteReaction()
    {
        for (int i = 0; i < inventoryCellsList.Count; i++)
        {
            inventoryCellsList[i].ResetSelectedState();
        }
    }

    public void ItemUsed_ExecuteReaction()
    {
        inventoryCellsList.Remove(currentlySelectedInventoryCell);
        Destroy(currentlySelectedInventoryCell.gameObject);
        _puzzleGamesEnvironmentsHolder.CurrentlyActiveGame.CollectableItemsManager.RemoveItemFromInventory(currentlySelectedInventoryCell.ItemType);
    }

    private void ChangeScrollButtonsState(bool isActive)
    {
        leftScrollButton.gameObject.SetActive(isActive);
        rightScrollButton.gameObject.SetActive(isActive);
    }

    private void SubscribeOnEvents()
    {
        rightScrollButton.OnScrollButtonPressed += RightScrollButtonPressed_ExecuteReaction;
        leftScrollButton.OnScrollButtonPressed += LeftScrollButtonPressed_ExecuteReaction;

        rightScrollButton.OnScrollButtonReleased += ScrollButtonReleased_ExecuteReaction;
        leftScrollButton.OnScrollButtonReleased += ScrollButtonReleased_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        rightScrollButton.OnScrollButtonPressed -= RightScrollButtonPressed_ExecuteReaction;
        leftScrollButton.OnScrollButtonPressed -= LeftScrollButtonPressed_ExecuteReaction;

        rightScrollButton.OnScrollButtonReleased -= ScrollButtonReleased_ExecuteReaction;
        leftScrollButton.OnScrollButtonReleased -= ScrollButtonReleased_ExecuteReaction;
    }

    private void RightScrollButtonPressed_ExecuteReaction()
    {
        //if (scrollRect.horizontalNormalizedPosition <= 0f)
        //{
            scrollRect.horizontalNormalizedPosition += scrollDelta;
        //}
    }

    private void LeftScrollButtonPressed_ExecuteReaction()
    {
        //if(scrollRect.horizontalNormalizedPosition >= 0f)
        //{
            scrollRect.horizontalNormalizedPosition -= scrollDelta;
        //}
    }

    private void ScrollButtonReleased_ExecuteReaction()
    {
        Debug.Log($"Stop");
    }
}
