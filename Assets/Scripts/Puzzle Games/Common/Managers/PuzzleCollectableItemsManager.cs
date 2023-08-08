using System;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCollectableItemsManager : MonoBehaviour
{
    [Header("Inventory Items")]
    [Space]
    [SerializeField] private List<PuzzleGameKitchenItems> itemsInInventoryList = new List<PuzzleGameKitchenItems>();
    [SerializeField] private List<PuzzleGameKitchenItems> usedItemsList = new List<PuzzleGameKitchenItems>();

    private PuzzleGameEnvironment levelEnvironment;

    public List<PuzzleGameKitchenItems> ItemsInInventoryList { get => itemsInInventoryList; private set => itemsInInventoryList = value; }
    public List<PuzzleGameKitchenItems> UsedItemsList { get => usedItemsList; private set => usedItemsList = value; }

    public void AddItemToInventory(PuzzleCollectableItem item)
    {
        itemsInInventoryList.Add(item.Item);
        levelEnvironment.UpdateEnvironmentSavedData();
    }

    public void RemoveItemFromInventory(PuzzleGameKitchenItems item)
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
            itemsInInventoryList.Add((PuzzleGameKitchenItems)collectedItemsIndexesList[i]);
        }

        for(int i = 0; i < usedItemsIndexesList.Count; i++)
        {
            usedItemsList.Add((PuzzleGameKitchenItems)usedItemsIndexesList[i]);
        }

        OnDataLoaded?.Invoke();
    }

    public void ResetData()
    {
        itemsInInventoryList.Clear();
        usedItemsList.Clear();
    }
}
