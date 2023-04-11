using UnityEngine;
using System;
using System.Collections;
using DG.Tweening;

public class PuzzleGame_MiniGameModePanel : PanelActivationManager
{
    [Header("Mini Game Data")]
    [Space]
    [SerializeField] private PuzzleGameKitchenMiniGames gameType;
    [Header("Mini Game Items")]
    [Space]
    [SerializeField] private MixerButton mixerButton;
    [SerializeField] private PuzzleKitchenBooksPositionsManager puzzleKitchenBooksPositionsManager;
    [SerializeField] private PopItGameStateManager popItGameStateManager;
    [SerializeField] private SafeButtonsHandler safeButtonsHandler;
    [Header("Scale Object")]
    [Space]
    [SerializeField] private Transform panelMainImage;
    [Header("Scale Data")]
    [Space]
    [SerializeField] private Vector3 minScaleVector = new Vector3(0.001f, 0.001f, 0.001f);
    [SerializeField] private float scaleDuration = 1f;

    public PuzzleGameKitchenMiniGames GameType { get => gameType; }
    public Transform PanelMainImage { get => panelMainImage; set => panelMainImage = value; }

    private Action _OnBookshelfGameComplete;

    private void Start()
    {
        if(panelMainImage)
        panelMainImage.localScale = minScaleVector;
    }

    public void StartMixerGame(Action OnMixerGameComplete)
    {
        if(mixerButton)
        {
            ScaleToStandartSize();
            mixerButton.StartGame(OnMixerGameComplete);
        }
    }

    public void StartBookshelfGame(Action OnBookshelfGameComplete)
    {
        if (puzzleKitchenBooksPositionsManager)
        {
            _OnBookshelfGameComplete = OnBookshelfGameComplete;
            puzzleKitchenBooksPositionsManager.SpawnBookshelfImage(CallBackFromBooksGame, minScaleVector);
            //StartCoroutine(StartBooksGameCoroutine(OnBookshelfGameComplete));
            //ScaleToStandartSize();
            //puzzleKitchenBooksPositionsManager.StartGame(OnBookshelfGameComplete);
        }
    }

    public void StartPopItGame(Action OnPopItGameComplete)
    {
        if (popItGameStateManager)
        {
            ScaleToStandartSize();
            popItGameStateManager.StartGame(OnPopItGameComplete);
        }
    }

    public void StartOpenSafeGame(Action OnOpenSafeGameComplete)
    {
        if (safeButtonsHandler)
        {
            ScaleToStandartSize();
            safeButtonsHandler.StartGame(OnOpenSafeGameComplete);
        }
    }

    public void ScaleToMinSize(Action OnComplete, Ease easeFunction)
    {
        panelMainImage.DOScale(minScaleVector, scaleDuration).SetEase(easeFunction).OnComplete(() =>
        {
            OnComplete?.Invoke();
        });
    }

    public void ScaleToStandartSize()
    {
        panelMainImage.DOScale(Vector3.one, scaleDuration).SetEase(Ease.OutBounce);
    }

    public void ResetMiniGamesPanelsUI()
    {
        if(mixerButton)
        {
            mixerButton.ResetGame();
        }
        if(puzzleKitchenBooksPositionsManager)
        {
            puzzleKitchenBooksPositionsManager.ResetGame();
        }
        if(popItGameStateManager)
        {
            popItGameStateManager.ResetGame();
        }
        if(safeButtonsHandler)
        {
            safeButtonsHandler.ResetGame();
        }
    }

    public void CallBackFromBooksGame()
    {
        StartCoroutine(StartBooksGameCoroutine(_OnBookshelfGameComplete));
    }

    private IEnumerator StartBooksGameCoroutine(Action OnBookshelfGameComplete)
    {
        yield return null;
        ScaleToStandartSize();
        puzzleKitchenBooksPositionsManager.StartGame(OnBookshelfGameComplete);
    }
}
