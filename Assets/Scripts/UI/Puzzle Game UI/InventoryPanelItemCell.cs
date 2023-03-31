using UnityEngine;
using UnityEngine.UI;

public class InventoryPanelItemCell : MonoBehaviour
{
    [Header("Item Data")]
    [Space]
    [SerializeField] private PuzzleGameKitchenItems itemType;
    [Header("Images")]
    [Space]
    [SerializeField] private Image itemImage;

    public PuzzleGameKitchenItems ItemType { get => itemType; private set => itemType = value; }

    public void SetItemData(Sprite sprite, PuzzleGameKitchenItems item)
    {
        itemImage.sprite = sprite;
        itemType = item;
    }
}
