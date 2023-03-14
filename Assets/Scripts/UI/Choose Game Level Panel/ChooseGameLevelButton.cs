using UnityEngine;

public class ChooseGameLevelButton : ButtonInteractionHandler
{
    [Header("Button Data")]
    [Space]
    [SerializeField] private GameLevelTypes gameType;
    [SerializeField] private int levelIndex;

    public override void ButtonActivated()
    {
        
    }

    public void SetButtonData(GameLevelTypes gameLevelType, int index)
    {
        gameType = gameLevelType;
        levelIndex = index;
    }
}
