using UnityEngine;
using System.Collections.Generic;
using System;
using DG.Tweening;
using TMPro;
using Zenject;

public class PuzzleGameUI : MainCanvasPanel
{
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI currentGameStopwatchText;
    [SerializeField] private TextMeshProUGUI delayGameTimerTitleText;
    [SerializeField] private TextMeshProUGUI startGameDelayTimerText;
    [Header("Delays")]
    [Space]
    [SerializeField] private float hideBonusTimeTextDelay = 2f;
    [Header("Durations")]
    [Space]
    [SerializeField] private float bonusTimeTextChangeAlphaDurationMax = 1f;
    [SerializeField] private float bonusTimeTextChangeAlphaDurationMin = 0.01f;
    [SerializeField] private float moveToInventoryPanelDuration = 1f;
    [Header("Inventory Panel")]
    [Space]
    [SerializeField] private PuzzleGameInventoryPanel inventoryPanel;
    [Header("Prefabs")]
    [Space]
    [SerializeField] private PuzzleKeyImage keyImagePrefab;
    [SerializeField] private PuzzleKeyImage colaStrawImagePrefab;
    [Header("Panels")]
    [Space]
    [SerializeField] private PanelActivationManager mainModePanel;
    [SerializeField] private PanelActivationManager gameFinishedPanel;
    [SerializeField] private List<PuzzleGame_MiniGameModePanel> minigamesPanelsList = new List<PuzzleGame_MiniGameModePanel>();
    [Header("Buttons")]
    [Space]
    [SerializeField] private PauseButton pauseButton;
    [SerializeField] private ClueButton clueButton;
    [SerializeField] private TogglePuzzleGame togglePuzzleGame;

    private PuzzleGamesEnvironmentsHolder _puzzleGamesEnvironmentsHolder;
    private TimersManager _timersManager;
    private PopItGameStateManager _popItGameStateManager;

    private Sprite newItemSprite;
    private PuzzleGameKitchenItems newItemType;

    public PuzzleGameInventoryPanel InventoryPanel { get => inventoryPanel; }
    public TogglePuzzleGame TogglePuzzleGame { get => togglePuzzleGame; }
    public PanelActivationManager GameFinishedPanel { get => gameFinishedPanel; }

    #region Events Declaration
    public event Action OnMixerGameFinished;
    public event Action OnBookshelfGameFinished;
    public event Action OnPopItGameFinished;
    public event Action OnOpenSafeGameFinished;
    #endregion Events Declaration

    private void Start()
    {
        mainModePanel.ShowPanel();
        HideMiniGamesPanels();
    }

    #region Zenject
    [Inject]
    private void Construct(PuzzleGamesEnvironmentsHolder puzzleGamesEnvironmentsHolder, TimersManager timersManager, PopItGameStateManager popItGameStateManager)
    {
        _puzzleGamesEnvironmentsHolder = puzzleGamesEnvironmentsHolder;
        _timersManager = timersManager;
        _popItGameStateManager = popItGameStateManager;
    }
    #endregion Zenject

    public void MoveKeyToInventoryBar(SpriteRenderer key, PuzzleGameKitchenItems item)
    {
        newItemSprite = key.sprite;
        newItemType = item;
        Vector3 spawnPos = Camera.main.WorldToScreenPoint(new Vector3(key.transform.position.x, key.transform.position.y, 0f));
        var keyImage = Instantiate(keyImagePrefab, spawnPos, Quaternion.identity, transform);
        keyImage.MoveToInventoryPanel(inventoryPanel.transform.position, key.sprite, item, this/*, ShowItemImageInInventoryPanel*/);
    }

    public void MoveItemToInventoryBar(SpriteRenderer itemSprite, PuzzleGameKitchenItems item)
    {
        newItemSprite = itemSprite.sprite;
        newItemType = item;
        Vector3 spawnPos = Camera.main.WorldToScreenPoint(new Vector3(itemSprite.transform.position.x, itemSprite.transform.position.y, 0f));
        if(item == PuzzleGameKitchenItems.ColaStraw)
        {
            var itemImage = Instantiate(colaStrawImagePrefab, spawnPos, Quaternion.identity, transform);
            itemImage.MoveToInventoryPanel(inventoryPanel.transform.position, itemSprite.sprite, item, this/*, ShowItemImageInInventoryPanel*/);
        }
    }

    public void ShowMainModePanel()
    {
        HideMiniGamesPanels();
        gameFinishedPanel.HidePanel();
        ShowAdditionalButtons();
        _puzzleGamesEnvironmentsHolder.GamesEnvironmentsList[0].InputManager.ChangeCheckInputState(true);
    }

    public void ShowGameFinishedPanel()
    {
        HideMiniGamesPanels();
        HideAdditionalButtons();
        gameFinishedPanel.ShowPanel();
    }

    public void ShowMiniGamePanel(PuzzleGameKitchenMiniGames gameType)
    {
        _puzzleGamesEnvironmentsHolder.CurrentlyActiveGame.InputManager.ChangeCheckInputState(false);
        mainModePanel.HidePanel();
        HideAdditionalButtons();

        for (int i = 0; i < minigamesPanelsList.Count; i++)
        {
            if (minigamesPanelsList[i].GameType != gameType)
            {
                minigamesPanelsList[i].HidePanel();
            }
            else
            {
                minigamesPanelsList[i].ShowPanel();

                if (gameType == PuzzleGameKitchenMiniGames.Mixer)
                {
                    minigamesPanelsList[i].StartMixerGame(MixerGameFinished_ExecuteReaction);
                }
                else if (gameType == PuzzleGameKitchenMiniGames.Bookshelf)
                {
                    minigamesPanelsList[i].StartBookshelfGame(BookshelfGameFinished_ExecuteReaction);
                }
                else if (gameType == PuzzleGameKitchenMiniGames.PopIt)
                {
                    minigamesPanelsList[i].StartPopItGame(PopItGameFinished_ExecuteReaction);
                }
                else if (gameType == PuzzleGameKitchenMiniGames.Safe)
                {
                    minigamesPanelsList[i].StartOpenSafeGame(OpenSafeGameFinished_ExecuteReaction);
                }
            }
        }
    }

    public void SetLevelLoadedData(PuzzleGameLevelData puzzleLevelLoadedData)
    {
        inventoryPanel.LoadCollectedItems(puzzleLevelLoadedData.itemsInInventoryList);
    }

    public void ShowItemImageInInventoryPanel(Sprite sprite, PuzzleGameKitchenItems item)
    {
        //inventoryPanel.PutItemInInventoryCell(newItemSprite, newItemType);
        inventoryPanel.PutItemInInventoryCell(sprite, item);
    }

    public void StartStopwatchCount(float startValue, Action<float> OnStopwatchStoped)
    {
        currentGameStopwatchText.text = $"{_timersManager.GetHoursAndMinutesAmount((int)startValue)}:{_timersManager.GetSecondsAmount((int)startValue)}";
        _timersManager.StartStopwatch(startValue, currentGameStopwatchText, OnStopwatchStoped);
    }

    public void ResetMiniGamesUI()
    {
        for(int i = 0; i < minigamesPanelsList.Count; i++)
        {
            minigamesPanelsList[i].ResetMiniGamesPanelsUI();
        }
        togglePuzzleGame.ResetGame();
    }

    private void MixerGameFinished_ExecuteReaction()
    {
        for(int i = 0; i < minigamesPanelsList.Count; i++)
        {
            if(minigamesPanelsList[i].GameType == PuzzleGameKitchenMiniGames.Mixer)
            {
                minigamesPanelsList[i].ScaleToMinSize(ShowMainModePanel, Ease.OutExpo);
                break;
            }
        }
        OnMixerGameFinished?.Invoke();
    }

    private void BookshelfGameFinished_ExecuteReaction()
    {
        for (int i = 0; i < minigamesPanelsList.Count; i++)
        {
            if (minigamesPanelsList[i].GameType == PuzzleGameKitchenMiniGames.Bookshelf)
            {
                minigamesPanelsList[i].ScaleToMinSize(ShowMainModePanel, Ease.InBack);
                break;
            }
        }
        OnBookshelfGameFinished?.Invoke();
    }

    private void PopItGameFinished_ExecuteReaction()
    {
        for (int i = 0; i < minigamesPanelsList.Count; i++)
        {
            if (minigamesPanelsList[i].GameType == PuzzleGameKitchenMiniGames.PopIt)
            {
                minigamesPanelsList[i].ScaleToMinSize(ShowMainModePanel, Ease.InBack);
                break;
            }
        }
        OnPopItGameFinished?.Invoke();
    }

    private void OpenSafeGameFinished_ExecuteReaction()
    {
        for (int i = 0; i < minigamesPanelsList.Count; i++)
        {
            if (minigamesPanelsList[i].GameType == PuzzleGameKitchenMiniGames.Safe)
            {
                minigamesPanelsList[i].ScaleToMinSize(ShowMainModePanel, Ease.InBack);
                break;
            }
        }
        OnOpenSafeGameFinished?.Invoke();
    }

    private void HideMiniGamesPanels()
    {
        mainModePanel.ShowPanel();

        for (int i = 0; i < minigamesPanelsList.Count; i++)
        {
            minigamesPanelsList[i].HidePanel();
        }
    }

    private void ShowAdditionalButtons()
    {
        pauseButton.gameObject.SetActive(true);
        clueButton.gameObject.SetActive(true);
    }

    private void HideAdditionalButtons()
    {
        pauseButton.gameObject.SetActive(false);
        clueButton.gameObject.SetActive(false);
    }
}
