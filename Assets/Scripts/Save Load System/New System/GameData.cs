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

    public List<MiniGameLevelData> miniGameLevelsDataList;

    public GameData()
    {
        languageIndex = 0;
        currentCoinsAmount = 0;
        soundMuted = false;
        miniGameLevelCoins = 0;
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
        levelIndex = 14;
        levelStateIndex = 14;
        highestScore = 14;
    }
}
