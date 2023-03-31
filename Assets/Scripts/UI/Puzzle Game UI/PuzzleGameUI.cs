using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
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
    [Header("Panels")]
    [Space]
    [SerializeField] private KitchenMiniGameBonusTimersPanel kitchenMiniGameBonusTimersPanel;
    [SerializeField] private PanelActivationManager gameFinishedPanelActivationManager;
    [Header("Inventory Panel")]
    [Space]
    [SerializeField] private PuzzleGameInventoryPanel inventoryPanel;
    [Header("Prefabs")]
    [Space]
    [SerializeField] private PuzzleKeyImage keyImagePrefab;
    [Header("Panels")]
    [Space]
    [SerializeField] private PanelActivationManager mainModePanel;
    [SerializeField] private List<PuzzleGame_MiniGameModePanel> minigamesPanelsList = new List<PuzzleGame_MiniGameModePanel>();

    private PuzzleGamesEnvironmentsHolder _puzzleGamesEnvironmentsHolder;

    private Sprite newItemSprite;
    private PuzzleGameKitchenItems newItemType;

    private void Start()
    {
        mainModePanel.ShowPanel();
        HideMiniGamesPanels();
    }

    #region Zenject
    [Inject]
    private void Construct(PuzzleGamesEnvironmentsHolder puzzleGamesEnvironmentsHolder)
    {
        _puzzleGamesEnvironmentsHolder = puzzleGamesEnvironmentsHolder;
    }
    #endregion Zenject

    public void MoveKeyToInventoryBar(SpriteRenderer key, PuzzleGameKitchenItems item)
    {
        newItemSprite = key.sprite;
        newItemType = item;
        Vector3 spawnPos = Camera.main.WorldToScreenPoint(new Vector3(key.transform.position.x, key.transform.position.y, 0f));
        var keyImage = Instantiate(keyImagePrefab, spawnPos, Quaternion.identity, transform);
        keyImage.MoveToInventoryPanel(inventoryPanel.transform.position, key.sprite, ShowKeyImageInInventoryPanel);
    }

    public void ShowMainModePanel()
    {
        HideMiniGamesPanels();
        _puzzleGamesEnvironmentsHolder.CurrentlyActiveGame.InputManager.ChangeCheckInputState(true);
    }

    public void ShowMiniGamePanel(PuzzleGameKitchenMiniGames gameType)
    {
        _puzzleGamesEnvironmentsHolder.CurrentlyActiveGame.InputManager.ChangeCheckInputState(false);
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
        inventoryPanel.LoadCollectedItems(puzzleLevelLoadedData.collectedItemsList);
    }

    private void ShowKeyImageInInventoryPanel()
    {
        inventoryPanel.PutItemInInventoryCell(newItemSprite, newItemType);
    }

    private void HideMiniGamesPanels()
    {
        for (int i = 0; i < minigamesPanelsList.Count; i++)
        {
            minigamesPanelsList[i].HidePanel();
        }
    }
}
