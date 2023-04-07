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
    private CameraManager _cameraManager;
    private PuzzleGamesEnvironmentsHolder _environmentsHolder;

    private float cameraMoveTowardsObjectDuration = 1f;

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
        _puzzleGameUI.OnPopItGameFinished += PopItGameFinished_ExecuteReaction;
        _puzzleGameUI.OnMixerGameFinished += MixerGameFinished_ExecuteReaction;
        _puzzleGameUI.OnBookshelfGameFinished += BookshelfGameFinished_ExecuteReaction;
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
        _puzzleGameUI.OnPopItGameFinished -= PopItGameFinished_ExecuteReaction;
        _puzzleGameUI.OnMixerGameFinished -= MixerGameFinished_ExecuteReaction;
        _puzzleGameUI.OnBookshelfGameFinished -= BookshelfGameFinished_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(PuzzleGameUI puzzleGameUI, PopItGameStateManager popItGameStateManager, CameraManager cameraManager, 
                           PuzzleGamesEnvironmentsHolder environmentsHolder)
    {
        _puzzleGameUI = puzzleGameUI;
        _popItGameStateManager = popItGameStateManager;
        _cameraManager = cameraManager;
        _environmentsHolder = environmentsHolder;
    }
    #endregion Zenject

    public void Interact()
    {
        if(containsKey && !key.gameObject.activeInHierarchy)
        {
            if (gameType == PuzzleGameKitchenMiniGames.PopIt || gameType == PuzzleGameKitchenMiniGames.Bookshelf || gameType == PuzzleGameKitchenMiniGames.Mixer)
            {
                _cameraManager.CameraMoveTo(transform, cameraMoveTowardsObjectDuration, ShowMiniGame);
            }
            else
            {
                ShowMiniGame();
            }
            //_puzzleGameUI.ShowMiniGamePanel(gameType);

            //if(gameType == PuzzleGameKitchenMiniGames.Window)
            //{
            //    if(TryGetComponent(out PuzzleWindowItem puzzleWindowItem))
            //    {
            //        puzzleWindowItem.WindowInteraction_ExecuteReaction();
            //    }
            //}
        }
    }

    public void ShowKey()
    {
        if(containsKey)
        {
            key.gameObject.SetActive(true);
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
        _environmentsHolder.CurrentlyActiveGame.InputManager.ChangeCheckInputState(true);
        if (gameType == PuzzleGameKitchenMiniGames.PopIt)
        {
            key.gameObject.SetActive(true);
        }
    }

    private void MixerGameFinished_ExecuteReaction()
    {
        _environmentsHolder.CurrentlyActiveGame.InputManager.ChangeCheckInputState(true);
        if (gameType == PuzzleGameKitchenMiniGames.Mixer)
        {
            key.gameObject.SetActive(true);
        }
    }

    private void BookshelfGameFinished_ExecuteReaction()
    {
        _environmentsHolder.CurrentlyActiveGame.InputManager.ChangeCheckInputState(true);
        if (gameType == PuzzleGameKitchenMiniGames.Bookshelf)
        {
            key.gameObject.SetActive(true);
        }
    }

    private void ShowMiniGame()
    {
        _puzzleGameUI.ShowMiniGamePanel(gameType);

        if (gameType == PuzzleGameKitchenMiniGames.Window)
        {
            if (TryGetComponent(out PuzzleWindowItem puzzleWindowItem))
            {
                puzzleWindowItem.WindowInteraction_ExecuteReaction();
            }
        }
    }

    private IEnumerator SetStartSettingsCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        if(key)
        key.gameObject.SetActive(false);
    }
}
