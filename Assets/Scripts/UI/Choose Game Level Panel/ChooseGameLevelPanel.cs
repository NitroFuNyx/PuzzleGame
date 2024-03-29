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
    [Header("Scale Coins Panel")]
    [Space]
    [SerializeField] private Vector3 coinsPanelScaleVector = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField] private float coinsPanelScaleDuration = 1f;
    [SerializeField] private int coinsPanelScaleFrequency = 4;
    [Header("External References")]
    [Space]
    [SerializeField] private TextsLanguageUpdateHandler_SelectModeUI textsLanguageUpdateHandler;

    private CurrentGameManager _currentGameManager;
    private ResourcesManager _resourcesManager;
    private DataPersistanceManager _dataPersistanceManager;
    private TimersManager _timersManager;

    private float fillPanelTextsDelay = 1f;

    public GameLevelStates LevelState { get => levelState; private set => levelState = value; }
    public GameLevelTypes GameType { get => gameType; }
    public int LevelPrice { get => levelPrice; }
    public int GameLevelIndex { get => gameLevelIndex; }
    public bool CanBeBought { get => canBeBought; private set => canBeBought = value; }

    private void Awake()
    {
        _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
    }

    private void Start()
    {
        //SetPanelUIData();
        StartCoroutine(FillPanelDataTextsCoroutine());
        levelButton.SetButtonData(this);
        StopBuyingPossibilityAnimation();
        

        _currentGameManager.OnGameLevelFinished += OnLevelFinished_ExecuteReaction;
        _currentGameManager.OnPuzzleLevelProgressUpdate += PuzzleLevelProgressUpdate_ExecuteReaction;
        _currentGameManager.OnPuzzleBestTimeDefined += PuzzleLevelBestTimeDefined_ExecuteReaction;

        _resourcesManager.OnAdditionalCoinsAdedAsAdReward += AdditionalResourcesGranted_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _currentGameManager.OnGameLevelFinished -= OnLevelFinished_ExecuteReaction;
        _currentGameManager.OnPuzzleLevelProgressUpdate -= PuzzleLevelProgressUpdate_ExecuteReaction;
        _currentGameManager.OnPuzzleBestTimeDefined -= PuzzleLevelBestTimeDefined_ExecuteReaction;

        _resourcesManager.OnAdditionalCoinsAdedAsAdReward -= AdditionalResourcesGranted_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(CurrentGameManager currentGameManager, DataPersistanceManager dataPersistanceManager, ResourcesManager resourcesManager,
                           TimersManager timersManager)
    {
        _currentGameManager = currentGameManager;
        _dataPersistanceManager = dataPersistanceManager;
        _resourcesManager = resourcesManager;
        _timersManager = timersManager;
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

    public void SetPanelUIData()
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
                timeTitle_Text.text = textsLanguageUpdateHandler.GetTranslatedText(SelectModePanelProgressTexts.Start);
                timeValue_Text.text = "";
            }
            else if (levelState == GameLevelStates.Available_Started)
            {
                timeTitle_Text.text = textsLanguageUpdateHandler.GetTranslatedText(SelectModePanelProgressTexts.CurrentTime);
                timeValue_Text.text = $"{_timersManager.GetHoursAndMinutesAmount((int)currentTimeInGame)}:{_timersManager.GetSecondsAmount((int)currentTimeInGame)}";
            }
            else if (levelState == GameLevelStates.Available_Finished)
            {
                if (gameType == GameLevelTypes.Puzzle)
                {
                    timeTitle_Text.text = textsLanguageUpdateHandler.GetTranslatedText(SelectModePanelProgressTexts.BestTime);
                    timeValue_Text.text = $"{_timersManager.GetHoursAndMinutesAmount((int)bestFinishTime)}:{_timersManager.GetSecondsAmount((int)bestFinishTime)}";
                    
                }
                else
                {
                    timeTitle_Text.text = textsLanguageUpdateHandler.GetTranslatedText(SelectModePanelProgressTexts.BestScore);
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

        SetBuyingPossibilityState();
    }

    private void PuzzleLevelProgressUpdate_ExecuteReaction(bool levelFinished, float time)
    {
        if(gameType == GameLevelTypes.Puzzle)
        {
            if (!levelFinished)
            {
                levelState = GameLevelStates.Available_Started;
                SetPanelUIData();
                currentTimeInGame = time;
                timeValue_Text.text = $"{_timersManager.GetHoursAndMinutesAmount((int)time)}:{_timersManager.GetSecondsAmount((int)time)}";
            }
            else
            {
                levelState = GameLevelStates.Available_Finished;
                SetPanelUIData();
            }
        }
    }

    private void PuzzleLevelBestTimeDefined_ExecuteReaction(float time)
    {
        if(gameType == GameLevelTypes.Puzzle)
        {
            if (levelState == GameLevelStates.Available_Finished)
            {
                SetPanelUIData();
                timeValue_Text.text = $"{_timersManager.GetHoursAndMinutesAmount((int)time)}:{_timersManager.GetSecondsAmount((int)time)}";
            }
        }
    }

    private void AdditionalResourcesGranted_ExecuteReaction(int amount)
    {
        SetBuyingPossibilityState();
    }

    public void SetBuyingPossibilityState()
    {
        if (_resourcesManager.WholeCoinsAmount >= levelPrice)
        {
            canBeBought = true;
        }
        else
        {
            canBeBought = false;
        }
    }

    public void ShowBuyingPossibilityState()
    {
        if(canBeBought)
        {
            costPanel.transform.DOPunchScale(coinsPanelScaleVector, coinsPanelScaleDuration, coinsPanelScaleFrequency).SetLoops(-1);
            lockImage.transform.DOPunchScale(coinsPanelScaleVector, coinsPanelScaleDuration, coinsPanelScaleFrequency).SetLoops(-1);
        }
        else
        {
            StopBuyingPossibilityAnimation();
        }
    }

    public void StopBuyingPossibilityAnimation()
    {
        costPanel.transform.DOKill();
        lockImage.transform.DOKill();
        costPanel.transform.localScale = Vector3.one;
        lockImage.transform.localScale = Vector3.one;
    }

    public void SetBoughtState()
    {
        levelState = GameLevelStates.Available_New;
        _resourcesManager.BuyLevel(levelPrice);
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
            levelState = (GameLevelStates)data.puzzleGameLevelsDataList[gameLevelIndex].levelStateIndex;
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
        else if (gameType == GameLevelTypes.Puzzle)
        {
            data.puzzleGameLevelsDataList[gameLevelIndex].levelStateIndex = (int)levelState;
        }
    }

    private IEnumerator UpdatePanelUICoroutine()
    {
        yield return null;
        SetBuyingPossibilityState();
        SetPanelUIData();
    }

    private IEnumerator FillPanelDataTextsCoroutine()
    {
        yield return new WaitForSeconds(fillPanelTextsDelay);
        SetPanelUIData();
    }
}
