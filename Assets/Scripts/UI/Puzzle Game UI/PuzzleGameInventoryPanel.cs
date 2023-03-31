using System.Collections.Generic;
using UnityEngine;

public class PuzzleGameInventoryPanel : MonoBehaviour
{
    [Header("Inventory Items Cells")]
    [Space]
    [SerializeField] private List<InventoryPanelItemCell> inventoryCellsList = new List<InventoryPanelItemCell>();
    [Header("Internal References")]
    [Space]
    [SerializeField] private Transform inventoryGrid;
    [Header("Prefabs")]
    [Space]
    [SerializeField] private InventoryPanelItemCell invenoryCellPrefab;
    [Header("Sprites")]
    [Space]
    [SerializeField] private List<Sprite> collectableItemsSpritesList = new List<Sprite>();

    public void PutItemInInventoryCell(Sprite sprite)
    {
        var cell = Instantiate(invenoryCellPrefab, Vector3.zero, Quaternion.identity, inventoryGrid);
        cell.SetItemImage(sprite);
    }

    public void LoadCollectedItems(List<int> collectedItemsList)
    {
        for(int i = 0; i < collectedItemsList.Count; i++)
        {
            Sprite itemSprite = collectableItemsSpritesList[collectedItemsList[i]];

            PutItemInInventoryCell(itemSprite);
        }
    }
}
