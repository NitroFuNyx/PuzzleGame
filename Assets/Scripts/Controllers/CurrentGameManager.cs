using UnityEngine;
using System;
using System.Collections;
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
    private PuzzleGamesEnvironmentsHolder _puzzleGamesEnvironments;
    private ResourcesManager _resourcesManager;
    private DataPersistanceManager _dataPersistanceManager;

    #region Events Declaration
    public event Action<CharacterTypes> OnCharacterChanged;
    public event Action<GameLevelTypes, int> OnGameLevelFinished;
    #endregion Events Declaration

    #region Zenject
    [Inject]
    private void Construct(MiniGamesEnvironmentsHolder miniGamesEnvironmentsHolder, PuzzleGamesEnvironmentsHolder puzzleGamesEnvironmentsHolder, ResourcesManager resourcesManager, DataPersistanceManager dataPersistanceManager)
    {
        _miniGamesEnvironmentsHolder = miniGamesEnvironmentsHolder;
        _puzzleGamesEnvironments = puzzleGamesEnvironmentsHolder;
        _resourcesManager = resourcesManager;
        _dataPersistanceManager = dataPersistanceManager;
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
        currentLevelIndex = levelIndex;

        if (currentGameType == GameLevelTypes.MiniGame)
        {
            _miniGamesEnvironmentsHolder.ActivateEnvironment(levelIndex);
        }
        else
        {
            _puzzleGamesEnvironments.ActivateEnvironment(levelIndex);
        }
    }

    public void SetGameFinisheData()
    {
        OnGameLevelFinished?.Invoke(currentGameType, currentLevelIndex);
        StartCoroutine(FinishGameWithSavingCoroutine());    
    }

    public void FinishGameWithoutSaving()
    {
        if(currentGameType == GameLevelTypes.MiniGame)
        {
            _miniGamesEnvironmentsHolder.CurrentlyActiveGame.ResetEnvironment();
        }
    }

    private IEnumerator FinishGameWithSavingCoroutine()
    {
        yield return new WaitForSeconds(1f);
        _dataPersistanceManager.SaveGame();

        yield return null;
        _miniGamesEnvironmentsHolder.CurrentlyActiveGame.ResetEnvironment();
    }
}
