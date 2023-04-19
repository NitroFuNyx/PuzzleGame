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

    private bool puzzleUIButtonPressed = false;
    private bool characterSet = false;

    public GameLevelTypes CurrentGameType { get => currentGameType; private set => currentGameType = value; }
    public CharacterTypes CurrentCharacter { get => currentCharacter; private set => currentCharacter = value; }
    public bool PuzzleUIButtonPressed { get => puzzleUIButtonPressed; set => puzzleUIButtonPressed = value; }

    private MiniGamesEnvironmentsHolder _miniGamesEnvironmentsHolder;
    private PuzzleGamesEnvironmentsHolder _puzzleGamesEnvironments;
    private ResourcesManager _resourcesManager;
    private DataPersistanceManager _dataPersistanceManager;
    private MainUI _mainUI;
    private AudioManager _audioManager;

    #region Events Declaration
    public event Action<CharacterTypes> OnCharacterChanged;
    public event Action<GameLevelTypes, int> OnGameLevelFinished;
    public event Action<bool, float> OnPuzzleLevelProgressUpdate;
    public event Action<float> OnPuzzleBestTimeDefined;
    #endregion Events Declaration

    #region Zenject
    [Inject]
    private void Construct(MiniGamesEnvironmentsHolder miniGamesEnvironmentsHolder, PuzzleGamesEnvironmentsHolder puzzleGamesEnvironmentsHolder, 
                           ResourcesManager resourcesManager, DataPersistanceManager dataPersistanceManager, MainUI mainUI, AudioManager audioManager)
    {
        _miniGamesEnvironmentsHolder = miniGamesEnvironmentsHolder;
        _puzzleGamesEnvironments = puzzleGamesEnvironmentsHolder;
        _resourcesManager = resourcesManager;
        _dataPersistanceManager = dataPersistanceManager;
        _mainUI = mainUI;
        _audioManager = audioManager;
    }
    #endregion Zenject

    public void SetCurrentGameType(GameLevelTypes choosenGame)
    {
        currentGameType = choosenGame;
    }

    public void SetCurrentCharacter(CharacterTypes choosenCharacter)
    {
        currentCharacter = choosenCharacter;
        currentGameType = GameLevelTypes.MiniGame;
        characterSet = true;
        OnCharacterChanged?.Invoke(currentCharacter);
        ActivateGameLevelEnvironment(currentLevelIndex, currentGameType);
    }

    public void ActivateGameLevelEnvironment(int levelIndex, GameLevelTypes gameType)
    {
        currentGameType = gameType;
        currentLevelIndex = levelIndex;
        if (currentGameType == GameLevelTypes.MiniGame)
        {
            if (!characterSet)
            {
                //_mainUI.ShowSelectCharacterUI();
                
                StartCoroutine(ShowDelayedMethodUICoroutine(_mainUI.ShowSelectCharacterUI));
            }
            else
            {
                _miniGamesEnvironmentsHolder.ActivateEnvironment(levelIndex);
                StartCoroutine(ShowDelayedMethodUICoroutine(_mainUI.ShowGameLevelUI));
                characterSet = false;
            }
        }
        else
        {
            _puzzleGamesEnvironments.ActivateEnvironment(levelIndex);
            StartCoroutine(ShowDelayedMethodUICoroutine(_mainUI.ShowGameLevelUI));
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
        else
        {
            _puzzleGamesEnvironments.CurrentlyActiveGame.UpdatePanelData();
            _puzzleGamesEnvironments.CurrentlyActiveGame.ResetEnvironmentWithoutSaving();
            // save time
        }

        StartCoroutine(ResetEnvironmentsCoroutine());
    }

    public void UpdatePuzzleLevelPanelData(bool levelFinished, float time)
    {
        OnPuzzleLevelProgressUpdate?.Invoke(levelFinished, time);
    }

    public void UpdatePuzzleBestTimeData(float time)
    {
        OnPuzzleBestTimeDefined?.Invoke(time);
    }

    public void HidePuzzleEnvironment()
    {
        Debug.Log($"Hide Puzzle");
        _puzzleGamesEnvironments.HideAllEnvironments();
    }

    public void HideMiniGameEnvironment()
    {
        Debug.Log($"Hide Mini Game");
        _miniGamesEnvironmentsHolder.HideAllEnvironments();
    }

    private IEnumerator FinishGameWithSavingCoroutine()
    {
        yield return new WaitForSeconds(1f);
        _dataPersistanceManager.SaveGame();

        yield return null;
        _miniGamesEnvironmentsHolder.CurrentlyActiveGame.ResetEnvironment();
    }

    private IEnumerator ResetEnvironmentsCoroutine()
    {
        //yield return new WaitForSeconds(2f);
        yield return null;
        _puzzleGamesEnvironments.HideAllEnvironments();
        _miniGamesEnvironmentsHolder.HideAllEnvironments();
    }

    private IEnumerator ShowDelayedMethodUICoroutine(Action OnDelayComplete)
    {
        yield return new WaitForSeconds(0.6f);
        OnDelayComplete?.Invoke();
    }
}
