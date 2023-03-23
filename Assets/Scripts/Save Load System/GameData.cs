using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData
{
    public int languageIndex;
    public int currentCoinsAmount;
    public bool soundMuted;

    public List<MiniGameLevelData> miniGameLevelsDataList;
    public List<PuzzleGameLevelData> puzzleGameLevelsDataList;

    public GameData(GameDataHolder gameDataHolder)
    {
        languageIndex = 0;
        currentCoinsAmount = 0;
        soundMuted = false;
        miniGameLevelsDataList = new List<MiniGameLevelData>();
        puzzleGameLevelsDataList = new List<PuzzleGameLevelData>();
}
}

[Serializable]
public class MiniGameLevelData
{
    public int levelIndex;
    public int levelStateIndex;
    public int highestScore;

    public MiniGameLevelData()
    {
        levelIndex = 0;
        levelStateIndex = 0;
        highestScore = 0;
    }
}

[Serializable]
public class PuzzleGameLevelData
{
    public int levelIndex;
    public int levelStateIndex;
    public int currentInGameTime;
    public int bestFinishTime;
    public List<int> usedKeysList;

    public PuzzleGameLevelData()
    {
        levelIndex = 0;
        levelStateIndex = 0;
        currentInGameTime = 0;
        bestFinishTime = 0;
        usedKeysList = new List<int>();
    }
}