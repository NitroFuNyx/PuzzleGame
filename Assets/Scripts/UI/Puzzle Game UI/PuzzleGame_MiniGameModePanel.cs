using UnityEngine;
using System;

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

    public PuzzleGameKitchenMiniGames GameType { get => gameType; }

    public void StartMixerGame(Action OnMixerGameComplete)
    {
        if(mixerButton)
        {
            mixerButton.StartGame(OnMixerGameComplete);
        }
    }

    public void StartBookshelfGame(Action OnBookshelfGameComplete)
    {
        if (puzzleKitchenBooksPositionsManager)
        {
            puzzleKitchenBooksPositionsManager.StartGame(OnBookshelfGameComplete);
        }
    }

    public void StartPopItGame(Action OnPopItGameComplete)
    {
        if (popItGameStateManager)
        {
            popItGameStateManager.StartGame(OnPopItGameComplete);
        }
    }
}
