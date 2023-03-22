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
    public int miniGameLevelCoins;

    public MiniGameLevelData[] miniGameLevelsDataList;

    public GameData(PlayerDataManager playerDataManager)
    {
        languageIndex = 0;
        currentCoinsAmount = 0;
        soundMuted = false;
        miniGameLevelCoins = 0;
        miniGameLevelsDataList = new MiniGameLevelData[playerDataManager.MiniGameLevelsPanelsList.Count];
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