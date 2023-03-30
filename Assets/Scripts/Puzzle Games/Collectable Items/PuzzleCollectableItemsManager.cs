using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCollectableItemsManager : MonoBehaviour
{
    [Header("Inventory Items")]
    [Space]
    [SerializeField] private List<PuzzleCollectableItem> itemsInInventoryList = new List<PuzzleCollectableItem>();

    private PuzzleGameEnvironment levelEnvironment;

    public List<PuzzleCollectableItem> ItemsInInventoryList { get => itemsInInventoryList; private set => itemsInInventoryList = value; }

    public void AddItemToInventory(PuzzleCollectableItem item)
    {
        itemsInInventoryList.Add(item);
        levelEnvironment.UpdateEnvironmentSavedData();
    }

    public void RemoveItemFromInventory(PuzzleCollectableItem item)
    {
        itemsInInventoryList.Remove(item);
    }

    public void CashComponents(PuzzleGameEnvironment puzzleGameEnvironment)
    {
        levelEnvironment = puzzleGameEnvironment;
    }
}
