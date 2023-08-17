using System;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCollectableItemsManager : MonoBehaviour
{
    [Header("Inventory Items")]
    [Space]
    [SerializeField] private List<PuzzleGameCollectableItems> itemsInInventoryList = new List<PuzzleGameCollectableItems>();
    [SerializeField] private List<PuzzleGameCollectableItems> usedItemsList = new List<PuzzleGameCollectableItems>();

    private PuzzleGameEnvironment levelEnvironment;

    public List<PuzzleGameCollectableItems> ItemsInInventoryList { get => itemsInInventoryList; private set => itemsInInventoryList = value; }
    public List<PuzzleGameCollectableItems> UsedItemsList { get => usedItemsList; private set => usedItemsList = value; }

    public void AddItemToInventory(PuzzleCollectableItem item)
    {
        itemsInInventoryList.Add(item.Item);
        levelEnvironment.UpdateEnvironmentSavedData();
    }

    public void RemoveItemFromInventory(PuzzleGameCollectableItems item)
    {
        itemsInInventoryList.Remove(item);
        usedItemsList.Add(item);
        levelEnvironment.UpdateEnvironmentSavedData();
    }

    public void CashComponents(PuzzleGameEnvironment puzzleGameEnvironment)
    {
        levelEnvironment = puzzleGameEnvironment;
    }

    public void LoadCollectedItemsData(List<int> collectedItemsIndexesList, List<int> usedItemsIndexesList, Action OnDataLoaded)
    {
        for(int i = 0; i < collectedItemsIndexesList.Count; i++)
        {
            itemsInInventoryList.Add((PuzzleGameCollectableItems)collectedItemsIndexesList[i]);
        }

        for(int i = 0; i < usedItemsIndexesList.Count; i++)
        {
            usedItemsList.Add((PuzzleGameCollectableItems)usedItemsIndexesList[i]);
        }

        OnDataLoaded?.Invoke();
    }

    public void ResetData()
    {
        itemsInInventoryList.Clear();
        usedItemsList.Clear();
    }
}
