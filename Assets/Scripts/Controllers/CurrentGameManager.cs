using UnityEngine;
using Zenject;

public class CurrentGameManager : MonoBehaviour
{
    [Header("Current Game Data")]
    [Space]
    [SerializeField] private GameLevelTypes currentGameType = GameLevelTypes.Puzzle;
    [SerializeField] private CharacterTypes currentCharacter = CharacterTypes.Female;

    public GameLevelTypes CurrentGameType { get => currentGameType; private set => currentGameType = value; }
    public CharacterTypes CurrentCharacter { get => currentCharacter; private set => currentCharacter = value; }

    private MiniGamesEnvironmentsHolder _miniGamesEnvironmentsHolder;

    #region Zenject
    [Inject]
    private void Construct(MiniGamesEnvironmentsHolder miniGamesEnvironmentsHolder)
    {
        _miniGamesEnvironmentsHolder = miniGamesEnvironmentsHolder;
    }
    #endregion Zenject

    public void SetCurrentGameType(GameLevelTypes choosenGame)
    {
        currentGameType = choosenGame;
    }

    public void SetCurrentCharacter(CharacterTypes choosenCharacter)
    {
        currentCharacter = choosenCharacter;
    }

    public void ActivateGameLevelEnvironment(int levelIndex)
    {
        if(currentGameType == GameLevelTypes.MiniGame)
        {
            _miniGamesEnvironmentsHolder.ActivateEnvironment(levelIndex);
        }
    }
}
