using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int languageIndex;
    public int currentCoinsAmount;
    public bool soundMuted;
    public int miniGameLevelCoins;

    public GameData()
    {
        languageIndex = 0;
        currentCoinsAmount = 0;
        soundMuted = false;
        miniGameLevelCoins = 0;
    }
}
