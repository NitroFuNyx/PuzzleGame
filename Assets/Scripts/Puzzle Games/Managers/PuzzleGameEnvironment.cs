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
    [Header("Environment Items With Keys")]
    [Space]
    [SerializeField] protected List<PuzzleKeyContainer> keyContainersList = new List<PuzzleKeyContainer>();
    [Header("Delays")]
    [Space]
    [SerializeField] private float finishGameDelay = 2f;

    private PuzzleGameUI _puzzleGameUI;
    private TimersManager _timersManager;
    private CurrentGameManager _currentGameManager;
    private DataPersistanceManager _dataPersistanceManager;
    private CameraManager _cameraManager;

    //private float startStopwatchValue = 0f;

    private float currentStopWatchValue;

    public int EnvironmentIndex { get => environmentIndex; }
    public PuzzleCollectableItemsManager CollectableItemsManager { get => collectableItemsManager; }
    public PuzzleInputManager InputManager { get => inputManager; }
    public PuzzleCluesManager CluesManager { get => cluesManager; }
    public PuzzleLocksHolder LocksHolder { get => locksHolder; }

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
                           DataPersistanceManager dataPersistanceManager, CameraManager cameraManager)
    {
        _timersManager = timersManager;
        _currentGameManager = currentGameManager;
        _puzzleGameUI = puzzleGameUI;
        _dataPersistanceManager = dataPersistanceManager;
        _cameraManager = cameraManager;
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

        for(int i = 0; i < collectableItemsManager.ItemsInInventoryList.Count; i++)
        {
            collectedItemsIndexesList.Add((int)collectableItemsManager.ItemsInInventoryList[i]);
        }

        for (int i = 0; i < collectableItemsManager.UsedItemsList.Count; i++)
        {
            usedItemsIndexesList.Add((int)collectableItemsManager.UsedItemsList[i]);
        }

        data.puzzleGameLevelsDataList[environmentIndex].itemsInInventoryList = collectedItemsIndexesList;
        data.puzzleGameLevelsDataList[environmentIndex].useditemsList = usedItemsIndexesList;

        data.puzzleGameLevelsDataList[0].currentInGameTime = currentStopWatchValue;
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
        inputManager.ChangeCheckInputState(false);
        StartCoroutine(FinishGameCoroutine());
    }

    private void OnStopwatchStoped_ExecuteReaction(float stopwatchValue)
    {
        Debug.Log($"Stopwatch {stopwatchValue}");
        currentStopWatchValue = stopwatchValue;
        string timeInForm = $"{_timersManager.GetHoursAndMinutesAmount((int)currentStopWatchValue)}:{_timersManager.GetSecondsAmount((int)currentStopWatchValue)}";
        _puzzleGameUI.GameFinishedPanel.GetComponent<PuzzleGameFinishedPanel>().SetFinishTimeText(timeInForm);
        _dataPersistanceManager.SaveGame();
    }

    private IEnumerator StartGameCoroutine()
    {
        yield return null;
        _puzzleGameUI.StartStopwatchCount(currentStopWatchValue, OnStopwatchStoped_ExecuteReaction);
        //float currentCounterValue = startGameDelay;
        //yield return new WaitForSeconds(startGameCoroutineDelay);

        //while (currentCounterValue > 0)
        //{
        //    _miniGameUI.UpdateStartGameDelayTimerText(currentCounterValue);
        //    currentCounterValue--;
        //    yield return new WaitForSeconds(1f);
        //    _miniGameUI.UpdateStartGameDelayTimerText(currentCounterValue);
        //}

        //yield return new WaitForSeconds(startGameCoroutineDelay);
        //_miniGameUI.HideDelayTimerText();
        //_miniGameUI.StartCurrentGameTimer(timeForLevel, TimerFinished_ExecuteReaction);
        //playerMoveManager.ChangeCheckingInputState(true);
        //playerCollisionManager.ChangeState_CanCollectItems(true);
        //kitchenMiniGameSpawnManager.StartSpawningItems();
    }

    private IEnumerator FinishGameCoroutine()
    {
        yield return new WaitForSeconds(finishGameDelay / 2);
        _timersManager.StopStopwatch();
        yield return new WaitForSeconds(finishGameDelay / 2);
        _puzzleGameUI.ShowGameFinishedPanel();
    }
}
