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
    [SerializeField] private PuzzleKey key;
    [SerializeField] private PuzzleKeyContainer keyContainerComponent;

    protected PuzzleGameUI _puzzleGameUI;
    private PopItGameStateManager _popItGameStateManager;

    private bool containsKey = true;

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
        _puzzleGameUI.ShowMiniGamePanel(gameType);
    }

    private void KeyCollected_ExecuteReaction()
    {
        containsKey = false;
    }

    private void CollectedItemsDataLoaded_ExecuteReaction(List<PuzzleGameKitchenItems> collectedItemsList)
    {
        if (key != null && collectedItemsList.Contains(key.Item))
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
