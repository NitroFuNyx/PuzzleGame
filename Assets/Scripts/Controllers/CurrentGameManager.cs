using UnityEngine;

public class CurrentGameManager : MonoBehaviour
{
    [Header("Current Game Data")]
    [Space]
    [SerializeField] private GameLevelTypes currentGameType = GameLevelTypes.Puzzle;
    [SerializeField] private CharacterTypes currentCharacter = CharacterTypes.Female;

    public GameLevelTypes CurrentGameType { get => currentGameType; private set => currentGameType = value; }
    public CharacterTypes CurrentCharacter { get => currentCharacter; private set => currentCharacter = value; }

    public void SetCurrentGameType(GameLevelTypes choosenGame)
    {
        currentGameType = choosenGame;
    }

    public void SetCurrentCharacter(CharacterTypes choosenCharacter)
    {
        currentCharacter = choosenCharacter;
    }
}
