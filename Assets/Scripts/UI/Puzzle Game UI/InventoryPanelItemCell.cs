using UnityEngine;
using UnityEngine.UI;

public class InventoryPanelItemCell : ButtonInteractionHandler
{
    [Header("Item Data")]
    [Space]
    [SerializeField] private PuzzleGameKitchenItems itemType;
    [Header("Images")]
    [Space]
    [SerializeField] private Image itemImage;
    [Header("Sprites")]
    [Space]
    [SerializeField] private Sprite standartSprite;
    [SerializeField] private Sprite selectedSprite;

    private PuzzleGameInventoryPanel inventoryPanel;

    private bool selected = false;

    public PuzzleGameKitchenItems ItemType { get => itemType; private set => itemType = value; }

    public override void ButtonActivated()
    {
        selected = !selected;

        if(selected)
        {
            buttonImage.sprite = selectedSprite;
            inventoryPanel.InventoryItemButtonPressed(this);
        }
        else
        {
            buttonImage.sprite = standartSprite;
        }
    }

    public void CashComponents(PuzzleGameInventoryPanel panel)
    {
        inventoryPanel = panel;
    }

    public void SetItemData(Sprite sprite, PuzzleGameKitchenItems item)
    {
        itemImage.sprite = sprite;
        itemType = item;
    }

    public void ResetSelectedState()
    {
        selected = false;
        buttonImage.sprite = standartSprite;
    }
}
