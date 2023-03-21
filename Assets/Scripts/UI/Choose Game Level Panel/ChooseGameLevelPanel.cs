using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Zenject;

public class ChooseGameLevelPanel : MonoBehaviour
{
    [Header("Game Level Data")]
    [Space]
    [SerializeField] private GameLevelStates levelState = GameLevelStates.Available_New;
    [SerializeField] private GameLevelTypes gameType = GameLevelTypes.Puzzle;
    [SerializeField] private int gameLevelIndex = 0;
    [Header("Button Images")]
    [Space]
    [SerializeField] private Image buttonImage;
    [SerializeField] private Image darkFilterImage;
    [SerializeField] private Image lockImage;
    [Header("Level Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI timeTitle_Text;
    [SerializeField] private TextMeshProUGUI timeValue_Text;
    [Header("Cost Panel")]
    [Space]
    [SerializeField] private PanelActivationManager costPanel;
    [SerializeField] private TextMeshProUGUI costAmountText;
    [Header("Internal References")]
    [Space]
    [SerializeField] private ChooseGameLevelButton levelButton;
    [Header("Durations")]
    [Space]
    [SerializeField] private float changeAlphaDuration = 0.01f;

    private CurrentGameManager _currentGameManager;
    private PlayerDataManager _playerDataManager;

    public GameLevelStates LevelState { get => levelState; set => levelState = value; }

    private void Start()
    {
        SetPanelUIData();
        levelButton.SetButtonData(levelState, gameType, gameLevelIndex);

        _currentGameManager.OnGameLevelFinished += OnLevelFinished_ExecuteReaction;
        _playerDataManager.OnPlayerMainDataLoaded += OnPlayerDataLoaded_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _currentGameManager.OnGameLevelFinished -= OnLevelFinished_ExecuteReaction;
        _playerDataManager.OnPlayerMainDataLoaded -= OnPlayerDataLoaded_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(CurrentGameManager currentGameManager, PlayerDataManager playerDataManager)
    {
        _currentGameManager = currentGameManager;
        _playerDataManager = playerDataManager;
    }
    #endregion Zenject

    public void SetPanelUIData()
    {
        if(levelState == GameLevelStates.Locked)
        {
            SetLockedStateUI();
        }
        else
        {
            SetAvailableStateUI();
        }
    }

    public void SetLevelStateIndex(int index)
    {
        if(index == 0)
        {
            levelState = GameLevelStates.Available_New;
        }
        else if (index == 1)
        {
            levelState = GameLevelStates.Available_Started;
        }
        if (index == 2)
        {
            levelState = GameLevelStates.Available_Finished;
        }
    }

    private void SetLockedStateUI()
    {
        darkFilterImage.DOFade(1f, changeAlphaDuration);
        lockImage.DOFade(1f, changeAlphaDuration);

        timeTitle_Text.text = "";
        timeValue_Text.text = "";

        costPanel.ShowPanel();

        // set cost text
    }

    private void SetAvailableStateUI()
    {
        darkFilterImage.DOFade(0f, changeAlphaDuration);
        lockImage.DOFade(0f, changeAlphaDuration);

        if (levelState == GameLevelStates.Available_New)
        {
            timeTitle_Text.text = "start";
            timeValue_Text.text = "";
        }
        else if(levelState == GameLevelStates.Available_Started)
        {
            timeTitle_Text.text = "current time";
            timeValue_Text.text = "01:01"; // set time
        }
        else if (levelState == GameLevelStates.Available_Finished)
        {
            if(gameType == GameLevelTypes.Puzzle)
            {
                timeTitle_Text.text = "best time";
                timeValue_Text.text = "01:01"; // set time
            }
            else
            {
                timeTitle_Text.text = "best score";
                timeValue_Text.text = $"{_playerDataManager.MiniGameLevelHighestScore}";
            }
        }

        costPanel.HidePanel();
    }

    private void OnLevelFinished_ExecuteReaction(GameLevelTypes finishedGameType, int finishedLevelIndex)
    {
        if(gameType == finishedGameType && gameLevelIndex == finishedLevelIndex)
        {
            levelState = GameLevelStates.Available_Finished;
            SetAvailableStateUI();
            _playerDataManager.SavePlayerData();
        }
    }

    private void OnPlayerDataLoaded_ExecuteReaction()
    {
        if (gameType == GameLevelTypes.MiniGame && gameLevelIndex == 0)
        {
            SetLevelStateIndex(_playerDataManager.MiniGameLevelStateIndex);
            SetAvailableStateUI();
        }
    }
}
