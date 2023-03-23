using UnityEngine;
using System.Collections.Generic;

public class GameDataHolder : MonoBehaviour
{
    [Header("Mini Game Levels Data")]
    [Space]
    [SerializeField] private List<ChooseGameLevelPanel> miniGameLevelsPanelsList = new List<ChooseGameLevelPanel>();
    [Header("Puzzle Game Levels Data")]
    [Space]
    [SerializeField] private List<ChooseGameLevelPanel> puzzleGameLevelsPanelsList = new List<ChooseGameLevelPanel>();

    public List<ChooseGameLevelPanel> MiniGameLevelsPanelsList { get => miniGameLevelsPanelsList; set => miniGameLevelsPanelsList = value; }
}
