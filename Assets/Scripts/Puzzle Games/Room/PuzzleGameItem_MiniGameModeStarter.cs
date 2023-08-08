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
    [Header("Bookshelf Data")]
    [Space]
    [SerializeField] private List<PuzzleSceneBook> greenShelfBooksList = new List<PuzzleSceneBook>();
    [SerializeField] private List<PuzzleSceneBook> yellowShelfBooksList = new List<PuzzleSceneBook>();
    [SerializeField] private List<PuzzleSceneBook> blueShelfBooksList = new List<PuzzleSceneBook>();

    protected PuzzleGameUI _puzzleGameUI;
    protected PopItGameStateManager _popItGameStateManager;
    private CameraManager _cameraManager;
    private PuzzleGamesEnvironmentsHolder _environmentsHolder;

    private SafeOpener _safeOpener;

    private float cameraMoveTowardsObjectDuration = 1f;

    private BoxCollider2D _collider;

    protected bool containsKey = true;

    private void Awake()
    {
        if(TryGetComponent(out BoxCollider2D boxCollider2D))
        {
            _collider = boxCollider2D;
        }
        if(TryGetComponent(out SafeOpener safeOpener))
        {
            _safeOpener = safeOpener;
        }
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
        _puzzleGameUI.OnPopItGameFinished += PopItGameFinished_ExecuteReaction;
        _puzzleGameUI.OnMixerGameFinished += MixerGameFinished_ExecuteReaction;
        _puzzleGameUI.OnBookshelfGameFinished += BookshelfGameFinished_ExecuteReaction;
        _puzzleGameUI.OnOpenSafeGameFinished += OpenSafeGameFinished_ExecuteReaction;
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
        _puzzleGameUI.OnPopItGameFinished -= PopItGameFinished_ExecuteReaction;
        _puzzleGameUI.OnMixerGameFinished -= MixerGameFinished_ExecuteReaction;
        _puzzleGameUI.OnBookshelfGameFinished -= BookshelfGameFinished_ExecuteReaction;
        _puzzleGameUI.OnOpenSafeGameFinished -= OpenSafeGameFinished_ExecuteReaction;
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
            if (gameType == PuzzleGameKitchenMiniGames.PopIt || gameType == PuzzleGameKitchenMiniGames.Bookshelf || gameType == PuzzleGameKitchenMiniGames.Mixer
                || gameType == PuzzleGameKitchenMiniGames.Safe)//TODO: CHANGE CAMERA CLAMP
            {
                _cameraManager.CameraMoveTo(transform, cameraMoveTowardsObjectDuration, ShowMiniGame);
            }
            else
            {
                ShowMiniGame();
            }
        }
    }

    public void ShowKey()
    {
        if(containsKey)
        {
            key.gameObject.SetActive(true);
            key.ChangeKeySimulattionState(true);
        }    
    }

    public void ChangeItemActivation(bool isActive)
    {
        if(_collider)
        {
            _collider.enabled = isActive;
        }
    }

    private void KeyCollected_ExecuteReaction()
    {
        containsKey = false;
        key.ChangeKeySimulattionState(false);
    }

    private void CollectedItemsDataLoaded_ExecuteReaction(List<PuzzleGameKitchenItems> collectedItemsList, List<PuzzleGameKitchenItems> usedItemsList)
    {
        if (key != null && (collectedItemsList.Contains(key.Item) || usedItemsList.Contains(key.Item)))
        {
            containsKey = false;
            key.ChangeKeySimulattionState(false);
            if (gameType == PuzzleGameKitchenMiniGames.Safe)
            {
                _safeOpener.OpenSafe();
            }
            
        }
    }

    public void ResetKeyData_ExecuteReaction()
    {
        containsKey = true;
        if (gameType == PuzzleGameKitchenMiniGames.Safe)
        {
            _safeOpener.CloseSafe();
        }
        ResetBooksSprites();
    }

    private void PopItGameFinished_ExecuteReaction()
    {
        _environmentsHolder.CurrentlyActiveGame.InputManager.ChangeCheckInputState(true);
        if (gameType == PuzzleGameKitchenMiniGames.PopIt)
        {
            ShowKey();
        }
    }

    private void MixerGameFinished_ExecuteReaction()
    {
        _environmentsHolder.CurrentlyActiveGame.InputManager.ChangeCheckInputState(true);
        if (gameType == PuzzleGameKitchenMiniGames.Mixer)
        {
            ShowKey();
        }
    }

    private void BookshelfGameFinished_ExecuteReaction()
    {
        _environmentsHolder.CurrentlyActiveGame.InputManager.ChangeCheckInputState(true);
        if (gameType == PuzzleGameKitchenMiniGames.Bookshelf)
        {
            ShowKey();

            SetBooksSpritesFinished();
        }
    }

    private void OpenSafeGameFinished_ExecuteReaction()
    {
        _environmentsHolder.CurrentlyActiveGame.InputManager.ChangeCheckInputState(true);
        if (gameType == PuzzleGameKitchenMiniGames.Safe)
        {
            ShowKey();
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

    private void ResetBooksSprites()
    {
        if(gameType == PuzzleGameKitchenMiniGames.Bookshelf)
        {
            for (int i = 0; i < greenShelfBooksList.Count; i++)
            {
                greenShelfBooksList[i].ChangeBookSpriteToStart();
            }
            for (int i = 0; i < yellowShelfBooksList.Count; i++)
            {
                yellowShelfBooksList[i].ChangeBookSpriteToStart();
            }
            for (int i = 0; i < blueShelfBooksList.Count; i++)
            {
                blueShelfBooksList[i].ChangeBookSpriteToStart();
            }
        }
    }

    private void SetBooksSpritesFinished()
    {
        if (gameType == PuzzleGameKitchenMiniGames.Bookshelf)
        {
            for (int i = 0; i < greenShelfBooksList.Count; i++)
            {
                greenShelfBooksList[i].ChangeSpriteToFinished(PuzzleGameKitchenBooks.Green);
            }
            for (int i = 0; i < yellowShelfBooksList.Count; i++)
            {
                yellowShelfBooksList[i].ChangeSpriteToFinished(PuzzleGameKitchenBooks.Yellow);
            }
            for (int i = 0; i < blueShelfBooksList.Count; i++)
            {
                blueShelfBooksList[i].ChangeSpriteToFinished(PuzzleGameKitchenBooks.Blue);
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
