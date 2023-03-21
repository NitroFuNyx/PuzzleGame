using System;

[Serializable]
public class PlayerMainData
{
    public int languageIndex;
    public int currentCoinsAmount;
    public bool soundMuted;
    public int miniGameLevelHighestScore;
    public int miniGameLevelStateIndex;

    public PlayerMainData(PlayerDataManager playerDataManager)
    {
        languageIndex = (int)playerDataManager.CurrentLanguage;
        currentCoinsAmount = playerDataManager.CurrentCoinsAmount;
        soundMuted = playerDataManager.SoundMuted;
        miniGameLevelHighestScore = playerDataManager.MiniGameLevelHighestScore;
        miniGameLevelStateIndex = playerDataManager.MiniGameLevelStateIndex;
    }
}
