using UnityEngine;
using System;
using System.Collections.Generic;

public class PuzzleKeyContainer : MonoBehaviour
{
    #region Events Declaration
    public event Action<List<PuzzleGameKitchenItems>> OnCollectedItemsDataLoaded;
    #endregion Events Declaration

    public void EnvironmentCollectedItemsDataLoaded(List<PuzzleGameKitchenItems> collectedItemsList)
    {
        OnCollectedItemsDataLoaded?.Invoke(collectedItemsList);
    }
}
