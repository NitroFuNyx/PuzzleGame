using System;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCollectableItemsManager : MonoBehaviour
{
    [Header("Inventory Items")]
    [Space]
    [SerializeField] private List<PuzzleGameKitchenItems> itemsInInventoryList = new List<PuzzleGameKitchenItems>();

    private PuzzleGameEnvironment levelEnvironment;

    public List<PuzzleGameKitchenItems> ItemsInInventoryList { get => itemsInInventoryList; private set => itemsInInventoryList = value; }

    public void AddItemToInventory(PuzzleCollectableItem item)
    {
        itemsInInventoryList.Add(item.Item);
        levelEnvironment.UpdateEnvironmentSavedData();
    }

    public void RemoveItemFromInventory(PuzzleGameKitchenItems item)
    {
        itemsInInventoryList.Remove(item);
        levelEnvironment.UpdateEnvironmentSavedData();
    }

    public void CashComponents(PuzzleGameEnvironment puzzleGameEnvironment)
    {
        levelEnvironment = puzzleGameEnvironment;
    }

    public void LoadCollectedItemsData(List<int> collectedItemsIndexesList, Action OnDataLoaded)
    {
        for(int i = 0; i < collectedItemsIndexesList.Count; i++)
        {
            itemsInInventoryList.Add((PuzzleGameKitchenItems)collectedItemsIndexesList[i]);
        }

        OnDataLoaded?.Invoke();
    }
}
