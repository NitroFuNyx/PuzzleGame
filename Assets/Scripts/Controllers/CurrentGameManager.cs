using UnityEngine;
using System;
using Zenject;

public class CurrentGameManager : MonoBehaviour
{
    [Header("Current Game Data")]
    [Space]
    [SerializeField] private GameLevelTypes currentGameType = GameLevelTypes.Puzzle;
    [SerializeField] private CharacterTypes currentCharacter = CharacterTypes.Female;
    [SerializeField] private int currentLevelIndex = 0;

    public GameLevelTypes CurrentGameType { get => currentGameType; private set => currentGameType = value; }
    public CharacterTypes CurrentCharacter { get => currentCharacter; private set => currentCharacter = value; }

    private MiniGamesEnvironmentsHolder _miniGamesEnvironmentsHolder;
    private PlayerDataManager _playerDataManager;
    private ResourcesManager _resourcesManager;

    #region Events Declaration
    public event Action<CharacterTypes> OnCharacterChanged;
    public event Action<GameLevelTypes, int> OnGameLevelFinished;
    #endregion Events Declaration

    #region Zenject
    [Inject]
    private void Construct(MiniGamesEnvironmentsHolder miniGamesEnvironmentsHolder, PlayerDataManager playerDataManager, ResourcesManager resourcesManager)
    {
        _miniGamesEnvironmentsHolder = miniGamesEnvironmentsHolder;
        _playerDataManager = playerDataManager;
        _resourcesManager = resourcesManager;
    }
    #endregion Zenject

    public void SetCurrentGameType(GameLevelTypes choosenGame)
    {
        currentGameType = choosenGame;
    }

    public void SetCurrentCharacter(CharacterTypes choosenCharacter)
    {
        currentCharacter = choosenCharacter;
        OnCharacterChanged?.Invoke(currentCharacter);
    }

    public void SetCurrentLevelIndex(int index)
    {
        currentLevelIndex = index;
    }

    public void ActivateGameLevelEnvironment(int levelIndex)
    {
        if(currentGameType == GameLevelTypes.MiniGame)
        {
            SetCurrentLevelIndex(levelIndex);
            _miniGamesEnvironmentsHolder.ActivateEnvironment(levelIndex);
        }
    }

    public void SetGameFinisheData()
    {
        _playerDataManager.SavePlayerData();
        OnGameLevelFinished?.Invoke(currentGameType, currentLevelIndex);
    }

    public void FinishGameWithoutSaving()
    {
        if(currentGameType == GameLevelTypes.MiniGame)
        {
            _resourcesManager.ResetCurrentLevelCoinsData();
            _miniGamesEnvironmentsHolder.CurrentlyActiveGame.ResetEnvironment();
        }
    }
}
