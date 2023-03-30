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

    public void PutItemInInventoryCell(Sprite sprite)
    {
        var cell = Instantiate(invenoryCellPrefab, Vector3.zero, Quaternion.identity, inventoryGrid);
        cell.SetItemImage(sprite);
    }
}
