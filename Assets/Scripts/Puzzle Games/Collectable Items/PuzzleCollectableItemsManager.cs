using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCollectableItemsManager : MonoBehaviour
{
    [Header("Inventory Items")]
    [Space]
    [SerializeField] private List<PuzzleCollectableItem> itemsInInventoryList = new List<PuzzleCollectableItem>();

    public void AddItemToInventory(PuzzleCollectableItem item)
    {
        itemsInInventoryList.Add(item);
    }

    public void RemoveItemFromInventory(PuzzleCollectableItem item)
    {
        itemsInInventoryList.Remove(item);
    }
}
