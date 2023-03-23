using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using DG.Tweening;
using Zenject;

public class ChooseGameLevelPanel : MonoBehaviour, IDataPersistance
{
    [Header("Game Level Data")]
    [Space]
    [SerializeField] private GameLevelStates levelState = GameLevelStates.Available_New;
    [SerializeField] private GameLevelTypes gameType = GameLevelTypes.Puzzle;
    [SerializeField] private int gameLevelIndex = 0;
    [SerializeField] private int highestScore = 0;
    [SerializeField] private float currentTimeInGame = 0;
    [SerializeField] private float bestFinishTime = 0;
    [SerializeField] private bool canBeBought = false;
    [SerializeField] private int levelPrice = 10000;
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
    [Header("Buttons")]
    [Space]
    [SerializeField] private ChooseGameLevelButton chooseGameLevelButton;

    private CurrentGameManager _currentGameManager;
    private ResourcesManager _resourcesManager;
    private DataPersistanceManager _dataPersistanceManager;

    public int LevelPrice { get => levelPrice; }

    private void Awake()
    {
        _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
    }

    private void Start()
    {
        SetPanelUIData();
        levelButton.SetButtonData(levelState, gameType, gameLevelIndex);

        _currentGameManager.OnGameLevelFinished += OnLevelFinished_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _currentGameManager.OnGameLevelFinished -= OnLevelFinished_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(CurrentGameManager currentGameManager, DataPersistanceManager dataPersistanceManager, ResourcesManager resourcesManager)
    {
        _currentGameManager = currentGameManager;
        _dataPersistanceManager = dataPersistanceManager;
        _resourcesManager = resourcesManager;
    }
    #endregion Zenject

    private void SetLockedStateUI()
    {
        darkFilterImage.DOFade(1f, changeAlphaDuration);
        lockImage.DOFade(1f, changeAlphaDuration);

        timeTitle_Text.text = "";
        timeValue_Text.text = "";

        costPanel.ShowPanel();

        costAmountText.text = $"{levelPrice}";

        if(canBeBought)
        {
            chooseGameLevelButton.SetCanBePurchasedState();
        }
    }

    private void SetPanelUIData()
    {
        darkFilterImage.DOFade(0f, changeAlphaDuration);
        lockImage.DOFade(0f, changeAlphaDuration);

        if(levelState == GameLevelStates.Locked)
        {
            SetLockedStateUI();
        }
        else
        {
            costPanel.HidePanel();

            if (levelState == GameLevelStates.Available_New)
            {
                timeTitle_Text.text = "start";
                timeValue_Text.text = "";
            }
            else if (levelState == GameLevelStates.Available_Started)
            {
                timeTitle_Text.text = "current time";
                timeValue_Text.text = "01:01"; // set time
            }
            else if (levelState == GameLevelStates.Available_Finished)
            {
                if (gameType == GameLevelTypes.Puzzle)
                {
                    timeTitle_Text.text = "best time";
                    timeValue_Text.text = "01:01"; // set time
                }
                else
                {
                    timeTitle_Text.text = "best score";
                    timeValue_Text.text = $"{highestScore}";
                }
            }
        }
    }

    private void OnLevelFinished_ExecuteReaction(GameLevelTypes finishedGameType, int finishedLevelIndex)
    {
        if(gameType == finishedGameType && gameLevelIndex == finishedLevelIndex)
        {
            if(highestScore < _resourcesManager.CurrentLevelCoinsAmount)
            {
                highestScore = _resourcesManager.CurrentLevelCoinsAmount;
            }

            levelState = GameLevelStates.Available_Finished;
            SetPanelUIData();
        }
    }

    private void SetBuyingPossibilityState()
    {
        if (_resourcesManager.WholeCoinsAmount > levelPrice)
        {
            canBeBought = true;
        }
        else
        {
            canBeBought = false;
        }
    }

    public void LoadData(GameData data)
    {
        if (gameType == GameLevelTypes.MiniGame)
        {
            highestScore = data.miniGameLevelsDataList[gameLevelIndex].highestScore;
            levelState = (GameLevelStates)data.miniGameLevelsDataList[gameLevelIndex].levelStateIndex;
        }
        else
        {
            currentTimeInGame = data.puzzleGameLevelsDataList[gameLevelIndex].currentInGameTime;
            bestFinishTime = data.puzzleGameLevelsDataList[gameLevelIndex].bestFinishTime;
        }

        StartCoroutine(UpdatePanelUICoroutine());
    }

    public void SaveData(GameData data)
    {
        if(gameType == GameLevelTypes.MiniGame)
        {
            data.miniGameLevelsDataList[gameLevelIndex].levelStateIndex = (int)levelState;
            data.miniGameLevelsDataList[gameLevelIndex].highestScore = highestScore;
        }
    }

    private IEnumerator UpdatePanelUICoroutine()
    {
        yield return null;
        SetBuyingPossibilityState();
        SetPanelUIData();
    }
}
