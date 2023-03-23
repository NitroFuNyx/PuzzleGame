using UnityEngine;
using System.Collections.Generic;

public class GameDataHolder : MonoBehaviour
{
    [Header("Mini Game Data")]
    [Space]
    [SerializeField] private List<ChooseGameLevelPanel> miniGameLevelsPanelsList = new List<ChooseGameLevelPanel>();
    [Header("Puzzle Game Data")]
    [Space]
    [SerializeField] private List<ChooseGameLevelPanel> puzzleGameLevelsPanelsList = new List<ChooseGameLevelPanel>();
    [SerializeField] private int allKeysAmount = 12;

    public List<ChooseGameLevelPanel> MiniGameLevelsPanelsList { get => miniGameLevelsPanelsList; }

    public List<ChooseGameLevelPanel> PuzzleGameLevelsPanelsList { get => puzzleGameLevelsPanelsList; }
    public int AllKeysAmount { get => allKeysAmount; }
}
