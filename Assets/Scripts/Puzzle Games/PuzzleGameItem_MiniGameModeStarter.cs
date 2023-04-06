using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;

public class PuzzleGameItem_MiniGameModeStarter : MonoBehaviour, Iinteractable
{
    [Header("Item Type")]
    [Space]
    [SerializeField] protected PuzzleGameKitchenMiniGames gameType;
    [Header("Key Data")]
    [Space]
    [SerializeField] protected PuzzleKey key;
    [SerializeField] protected PuzzleKeyContainer keyContainerComponent;

    protected PuzzleGameUI _puzzleGameUI;
    protected PopItGameStateManager _popItGameStateManager;

    protected bool containsKey = true;

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
        }
        _popItGameStateManager.OnGameFinished += PopItGameFinished_ExecuteReaction;
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
        }
        _popItGameStateManager.OnGameFinished -= PopItGameFinished_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(PuzzleGameUI puzzleGameUI, PopItGameStateManager popItGameStateManager)
    {
        _puzzleGameUI = puzzleGameUI;
        _popItGameStateManager = popItGameStateManager;
    }
    #endregion Zenject

    public void Interact()
    {
        if(containsKey)
        {
            _puzzleGameUI.ShowMiniGamePanel(gameType);

            if(gameType == PuzzleGameKitchenMiniGames.Window)
            {
                if(TryGetComponent(out PuzzleWindowItem puzzleWindowItem))
                {
                    puzzleWindowItem.WindowInteraction_ExecuteReaction();
                }
            }
        }
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
        }
    }

    private void PopItGameFinished_ExecuteReaction()
    {
        if(gameType == PuzzleGameKitchenMiniGames.PopIt)
        {
            key.gameObject.SetActive(true);
        }
    }

    private IEnumerator SetStartSettingsCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        if(key)
        key.gameObject.SetActive(false);
    }
}
