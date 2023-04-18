using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Zenject;

public class PuzzleGameEnvironment : MonoBehaviour, IDataPersistance
{
    [Header("Environment Data")]
    [Space]
    [SerializeField] private int environmentIndex;
    [Header("Time Data")]
    [Space]
    [SerializeField] private float startGameDelay = 3f;
    [SerializeField] private float startGameCoroutineDelay = 0.5f;
    [SerializeField] private float startTime = 0f;
    [Header("Internal References")]
    [Space]
    [SerializeField] private PuzzleInputManager inputManager;
    [SerializeField] private PuzzleCollectableItemsManager collectableItemsManager;
    [SerializeField] private PuzzleCluesManager cluesManager;
    [SerializeField] private PuzzleLocksHolder locksHolder;
    [SerializeField] private PuzzleKitchenCake cake;
    [Header("Environment Items With Keys")]
    [Space]
    [SerializeField] protected List<PuzzleKeyContainer> keyContainersList = new List<PuzzleKeyContainer>();
    [Header("Delays")]
    [Space]
    [SerializeField] private float finishGameDelay = 2f;
    [Header("Items For Reset")]
    [Space]
    [SerializeField] private List<PuzzleGameItem_DoorFurniture> furnitureItemsList;
    [SerializeField] private List<PuzzleKey> keysList;
    [SerializeField] private PuzzleGamePaintingsHolder paintingsHolder;
    [SerializeField] private PuzzleGameKitchenFlower flower;
    [SerializeField] private PuzzleColaGlass colaGlass;
    [SerializeField] private PuzzleWindowItem windowItem;
    [SerializeField] private PuzzleGameKitchenRag rag;

    private PuzzleGameUI _puzzleGameUI;
    private TimersManager _timersManager;
    private CurrentGameManager _currentGameManager;
    private DataPersistanceManager _dataPersistanceManager;
    private CameraManager _cameraManager;
    private AudioManager _audioManager;

    [SerializeField] private float currentStopWatchValue;

    private bool gameFinished = false;

    public int EnvironmentIndex { get => environmentIndex; }
    public PuzzleCollectableItemsManager CollectableItemsManager { get => collectableItemsManager; }
    public PuzzleInputManager InputManager { get => inputManager; }
    public PuzzleCluesManager CluesManager { get => cluesManager; }
    public PuzzleLocksHolder LocksHolder { get => locksHolder; }
    public bool GameFinished { get => gameFinished; private set => gameFinished = value; }

    #region Events Declaration
    public event Action OnLockOpened;
    public event Action OnLockKeyMismatched;
    #endregion Events Declaration

    private void Awake()
    {
        _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
        collectableItemsManager.CashComponents(this);
        cluesManager.SetEnvironmentIndex(environmentIndex);
    }

    private void Start()
    {
        SubscribeOnEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    #region Zenject
    [Inject]
    private void Construct(TimersManager timersManager, CurrentGameManager currentGameManager, PuzzleGameUI puzzleGameUI, 
                           DataPersistanceManager dataPersistanceManager, CameraManager cameraManager, AudioManager audioManager)
    {
        _timersManager = timersManager;
        _currentGameManager = currentGameManager;
        _puzzleGameUI = puzzleGameUI;
        _dataPersistanceManager = dataPersistanceManager;
        _cameraManager = cameraManager;
        _audioManager = audioManager;
    }
    #endregion Zenject

    public void StartGame()
    {
        inputManager.ChangeCheckInputState(true);
        StartCoroutine(StartGameCoroutine());
    }

    public void UpdateEnvironmentSavedData()
    {
        _dataPersistanceManager.SaveGame();
    }

    public void ResetEnvironmentWithoutSaving()
    {
        _cameraManager.SetCameraStartPos();
        _timersManager.StopStopwatch();
    }

    private void SubscribeOnEvents()
    {
        
    }

    private void UnsubscribeFromEvents()
    {
        
    }

    public void LoadData(GameData data)
    {
        _puzzleGameUI.SetLevelLoadedData(data.puzzleGameLevelsDataList[environmentIndex]); 
        collectableItemsManager.LoadCollectedItemsData(data.puzzleGameLevelsDataList[environmentIndex].itemsInInventoryList,
                                                       data.puzzleGameLevelsDataList[environmentIndex].useditemsList, CollectedItemsDataLoaded_ExecuteReaction);

        currentStopWatchValue = data.puzzleGameLevelsDataList[0].currentInGameTime;

        if(data.puzzleGameLevelsDataList[0].levelStateIndex == (int)GameLevelStates.Available_Finished)
        {
            currentStopWatchValue = 0f;
        }
    }

    public void SaveData(GameData data)
    {
        List<int> collectedItemsIndexesList = new List<int>();
        List<int> usedItemsIndexesList = new List<int>();
        
        if(!gameFinished)
        {
            for (int i = 0; i < collectableItemsManager.ItemsInInventoryList.Count; i++)
            {
                collectedItemsIndexesList.Add((int)collectableItemsManager.ItemsInInventoryList[i]);
            }

            for (int i = 0; i < collectableItemsManager.UsedItemsList.Count; i++)
            {
                usedItemsIndexesList.Add((int)collectableItemsManager.UsedItemsList[i]);
            }
        }
        else
        {
            collectableItemsManager.ResetData();
            
            if(data.puzzleGameLevelsDataList[0].bestFinishTime == 0f)
            {
                data.puzzleGameLevelsDataList[0].bestFinishTime = currentStopWatchValue;
            }
            else if(currentStopWatchValue < data.puzzleGameLevelsDataList[0].bestFinishTime && data.puzzleGameLevelsDataList[0].bestFinishTime != 0f)
            {
                data.puzzleGameLevelsDataList[0].bestFinishTime = currentStopWatchValue;
            }
            _currentGameManager.UpdatePuzzleLevelPanelData(gameFinished, currentStopWatchValue);
            _currentGameManager.UpdatePuzzleBestTimeData(data.puzzleGameLevelsDataList[0].bestFinishTime);

            currentStopWatchValue = 0f;
        }

        data.puzzleGameLevelsDataList[0].currentInGameTime = currentStopWatchValue;
        data.puzzleGameLevelsDataList[environmentIndex].itemsInInventoryList = collectedItemsIndexesList;
        data.puzzleGameLevelsDataList[environmentIndex].useditemsList = usedItemsIndexesList;

        //data.puzzleGameLevelsDataList[0].currentInGameTime = currentStopWatchValue;
    }

    public void CollectedItemsDataLoaded_ExecuteReaction()
    {
        for (int i = 0; i < keyContainersList.Count; i++)
        {
            keyContainersList[i].EnvironmentCollectedItemsDataLoaded(collectableItemsManager.ItemsInInventoryList, collectableItemsManager.UsedItemsList);
        }
    }

    [ContextMenu("Finish")]
    public void AllLocksOpened_ExecuteReaction()
    {
        //gameFinished = true;
        inputManager.ChangeCheckInputState(false);
        StartCoroutine(FinishGameCoroutine());
        _puzzleGameUI.HideAdditionalButtons();
    }

    public void FullResetEnvironment()
    {
        for(int i = 0; i < keyContainersList.Count; i++)
        {
            keyContainersList[i].ResetKeyData();
        }

        for(int i = 0; i < furnitureItemsList.Count; i++)
        {
            furnitureItemsList[i].ResetItem();
        }

        for(int i = 0; i < keysList.Count; i++)
        {
            keysList[i].ResetKey();
        }

        _puzzleGameUI.ResetMiniGamesUI();
        paintingsHolder.ResetPaintings();
        flower.ResetFlower();
        colaGlass.ResetItem();
        windowItem.ResetItem();
        locksHolder.ResetLocks();
        rag.ResetItem();
        //_dataPersistanceManager.SaveGame();
    }

    public void UpdatePanelData()
    {
        _currentGameManager.UpdatePuzzleLevelPanelData(gameFinished, currentStopWatchValue);
    }

    private void OnStopwatchStoped_ExecuteReaction(float stopwatchValue)
    {
        currentStopWatchValue = stopwatchValue;
        
        _currentGameManager.UpdatePuzzleLevelPanelData(gameFinished, currentStopWatchValue);

        if (gameFinished)
        {
            string timeInForm = $"{_timersManager.GetHoursAndMinutesAmount((int)currentStopWatchValue)}:{_timersManager.GetSecondsAmount((int)currentStopWatchValue)}";
            _puzzleGameUI.GameFinishedPanel.GetComponent<PuzzleGameFinishedPanel>().SetFinishTimeText(timeInForm);
            //FullResetEnvironment();
            _dataPersistanceManager.SaveGame();
            StartCoroutine(FullResetCoroutine());
        }
        else
        {
            _dataPersistanceManager.SaveGame();
        }
        //_dataPersistanceManager.SaveGame();
    }

    private IEnumerator StartGameCoroutine()
    {
        yield return null;
        _puzzleGameUI.StartStopwatchCount(currentStopWatchValue, OnStopwatchStoped_ExecuteReaction);
        gameFinished = false;
    }

    private IEnumerator FinishGameCoroutine()
    {
        _audioManager.PlayVoicesAudio_EndGame();
        cake.SetAnimationState_Jump();
        yield return new WaitForSeconds(finishGameDelay / 2);
        gameFinished = true;
        _timersManager.StopStopwatch();
        yield return new WaitForSeconds(finishGameDelay / 2);
        _puzzleGameUI.ShowGameFinishedPanel();
        _cameraManager.SetCameraStartPos();
    }

    private IEnumerator FullResetCoroutine()
    {
        yield return new WaitForSeconds(finishGameDelay);
        yield return null;
        FullResetEnvironment();
    }
}
