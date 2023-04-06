using UnityEngine;

public class PuzzleGame_MiniGameModePanel : PanelActivationManager
{
    [Header("Mini Game Data")]
    [Space]
    [SerializeField] private PuzzleGameKitchenMiniGames gameType;

    public PuzzleGameKitchenMiniGames GameType { get => gameType; }

    
    
}
