using UnityEngine;
using System;
using System.Collections.Generic;

public class PuzzleKeyContainer : MonoBehaviour
{
    #region Events Declaration
    public event Action<List<PuzzleGameCollectableItems>, List<PuzzleGameCollectableItems>> OnCollectedItemsDataLoaded;
    public event Action OnKeyDataReset;
    #endregion Events Declaration

    public void EnvironmentCollectedItemsDataLoaded(List<PuzzleGameCollectableItems> collectedItemsList, List<PuzzleGameCollectableItems> usedItemsList)
    {
        OnCollectedItemsDataLoaded?.Invoke(collectedItemsList, usedItemsList);
    }

    public void ResetKeyData()
    {
        OnKeyDataReset?.Invoke();
    }
}
