using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PuzzleColaGlass : MonoBehaviour, IDataPersistance, Iinteractable
{
    [Header("Additional Items")]
    [Space]
    [SerializeField] private PuzzleColaStraw straw;
    [Header("Internal References")]
    [Space]
    [SerializeField] private PuzzleKey key;
    [SerializeField] private PuzzleKeyContainer keyContainerComponent;

    private DataPersistanceManager _dataPersistanceManager;
    private PuzzleGameUI _puzzleGameUI;

    private bool containsKey = true;

    private void Awake()
    {
        _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
        straw.CashComponents(this);
    }

    private void Start()
    {
        if (TryGetComponent(out PuzzleClueHolder clueHolder))
        {
            clueHolder.ClueIndex = key.KeyIndex;
        }
        if (key)
        {
            key.OnKeyCollected += KeyCollected_ExecuteReaction;
        }
        if (keyContainerComponent)
        {
            keyContainerComponent.OnCollectedItemsDataLoaded += CollectedItemsDataLoaded_ExecuteReaction;
            keyContainerComponent.OnKeyDataReset += ResetKeyData_ExecuteReaction;
        }
        StartCoroutine(SetStartSettingsCoroutine());
    }

    private void OnDestroy()
    {
        if (key)
        {
            key.OnKeyCollected -= KeyCollected_ExecuteReaction;
        }
        if (keyContainerComponent)
        {
            keyContainerComponent.OnCollectedItemsDataLoaded -= CollectedItemsDataLoaded_ExecuteReaction;
            keyContainerComponent.OnKeyDataReset -= ResetKeyData_ExecuteReaction;
        }
    }

    #region Zenject
    [Inject]
    private void Construct(DataPersistanceManager dataPersistanceManager, PuzzleGameUI puzzleGameUI)
    {
        _dataPersistanceManager = dataPersistanceManager;
        _puzzleGameUI = puzzleGameUI;
    }
    #endregion Zenject

    public void LoadData(GameData data)
    {
        if (data.puzzleGameLevelsDataList[0].itemsInInventoryList.Contains((int)PuzzleGameKitchenItems.ColaStraw))
        {
            straw.gameObject.SetActive(false);
        }
    }

    public void SaveData(GameData data)
    {
        
    }

    public void ResetItem()
    {
        straw.gameObject.SetActive(true);
    }

    private void KeyCollected_ExecuteReaction()
    {
        containsKey = false;
    }

    private void CollectedItemsDataLoaded_ExecuteReaction(List<PuzzleGameKitchenItems> collectedItemsList, List<PuzzleGameKitchenItems> usedItemsList)
    {
        if (key != null && (collectedItemsList.Contains(key.Item) || usedItemsList.Contains(key.Item)))
        {
            containsKey = false;
            key.ChangeKeySimulattionState(false);
            straw.gameObject.SetActive(false);
        }
    }

    public void ResetKeyData_ExecuteReaction()
    {
        containsKey = true;
    }

    private IEnumerator SetStartSettingsCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        key.gameObject.SetActive(false);
        key.ChangeKeySimulattionState(false);
    }

    public void Interact()
    {
        if (containsKey && _puzzleGameUI.InventoryPanel.CurrentlySelectedInventoryCell != null)
        {
            if(_puzzleGameUI.InventoryPanel.CurrentlySelectedInventoryCell.ItemType == PuzzleGameKitchenItems.ColaStraw)
            {
                key.gameObject.SetActive(true);
                key.ChangeKeySimulattionState(true);
                _puzzleGameUI.InventoryPanel.ItemUsed_ExecuteReaction();
            }
        }
    }
}
