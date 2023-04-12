using UnityEngine;

public class GameLevelCostButton : ButtonInteractionHandler
{
    [Header("External References")]
    [Space]
    [SerializeField] private ChooseGameLevelButton chooseGameLevelButton;

    public override void ButtonActivated()
    {
        chooseGameLevelButton.ButtonActivated();
    }
}
