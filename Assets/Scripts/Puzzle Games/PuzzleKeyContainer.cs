using UnityEngine;
using System;
using System.Collections.Generic;

public class PuzzleKeyContainer : MonoBehaviour
{
    #region Events Declaration
    public event Action<List<PuzzleGameKitchenItems>, List<PuzzleGameKitchenItems>> OnCollectedItemsDataLoaded;
    public event Action OnKeyDataReset;
    #endregion Events Declaration

    public void EnvironmentCollectedItemsDataLoaded(List<PuzzleGameKitchenItems> collectedItemsList, List<PuzzleGameKitchenItems> usedItemsList)
    {
        OnCollectedItemsDataLoaded?.Invoke(collectedItemsList, usedItemsList);
    }

    public void ResetKeyData()
    {
        OnKeyDataReset?.Invoke();
    }
}
