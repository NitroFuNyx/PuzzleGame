using UnityEngine;
using System.Collections.Generic;
using System;
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

    private PuzzleGamesEnvironmentsHolder _puzzleGamesEnvironmentsHolder;
    private TimersManager _timersManager;

    private Sprite newItemSprite;
    private PuzzleGameKitchenItems newItemType;

    public PuzzleGameInventoryPanel InventoryPanel { get => inventoryPanel; }

    private void Start()
    {
        mainModePanel.ShowPanel();
        HideMiniGamesPanels();
    }

    #region Zenject
    [Inject]
    private void Construct(PuzzleGamesEnvironmentsHolder puzzleGamesEnvironmentsHolder, TimersManager timersManager)
    {
        _puzzleGamesEnvironmentsHolder = puzzleGamesEnvironmentsHolder;
        _timersManager = timersManager;
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
        ShowAdditionalButtons();
        _puzzleGamesEnvironmentsHolder.CurrentlyActiveGame.InputManager.ChangeCheckInputState(true);
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
        pauseButton.ShowButton();
        //clueButton.ShowButton();
    }

    private void HideAdditionalButtons()
    {
        pauseButton.HideButton();
        //clueButton.HideButton();
    }
}
