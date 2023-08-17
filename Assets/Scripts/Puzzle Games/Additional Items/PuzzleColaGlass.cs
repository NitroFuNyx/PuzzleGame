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
    [Header("Sprites")]
    [Space]
    [SerializeField] private Sprite colaFullSprite;
    [SerializeField] private Sprite colaEmptySprite;

    private DataPersistanceManager _dataPersistanceManager;
    private PuzzleGameUI _puzzleGameUI;

    private SpriteRenderer spriteRenderer;

    private bool containsKey = true;

    private void Awake()
    {
        _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
        straw.CashComponents(this);
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (data.puzzleGameLevelsDataList[0].itemsInInventoryList.Contains((int)PuzzleGameCollectableItems.ColaStraw))
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
        spriteRenderer.sprite = colaFullSprite;
        straw.ResetItem();
    }

    private void KeyCollected_ExecuteReaction()
    {
        containsKey = false;
    }

    private void CollectedItemsDataLoaded_ExecuteReaction(List<PuzzleGameCollectableItems> collectedItemsList, List<PuzzleGameCollectableItems> usedItemsList)
    {
        if (key != null && (collectedItemsList.Contains(key.Item) || usedItemsList.Contains(key.Item)))
        {
            containsKey = false;
            key.ChangeKeySimulattionState(false);
            straw.gameObject.SetActive(false);
            spriteRenderer.sprite = colaEmptySprite;
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
            if(_puzzleGameUI.InventoryPanel.CurrentlySelectedInventoryCell.ItemType == PuzzleGameCollectableItems.ColaStraw)
            {
                key.gameObject.SetActive(true);
                spriteRenderer.sprite = colaEmptySprite;
                key.ChangeKeySimulattionState(true);
                _puzzleGameUI.InventoryPanel.ItemUsed_ExecuteReaction();
            }
        }
    }
}
