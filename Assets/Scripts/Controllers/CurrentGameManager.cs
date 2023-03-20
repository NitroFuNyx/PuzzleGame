using UnityEngine;
using System;
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
    private PlayerDataManager _playerDataManager;
    private ResourcesManager _resourcesManager;

    #region Events Declaration
    public event Action<CharacterTypes> OnCharacterChanged;
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

    public void ActivateGameLevelEnvironment(int levelIndex)
    {
        if(currentGameType == GameLevelTypes.MiniGame)
        {
            _miniGamesEnvironmentsHolder.ActivateEnvironment(levelIndex);
        }
    }

    public void SetGameFinisheData()
    {
        _playerDataManager.SavePlayerData();
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
