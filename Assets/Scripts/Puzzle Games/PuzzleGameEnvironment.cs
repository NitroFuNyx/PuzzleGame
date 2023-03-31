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
    [Header("Environment Items With Keys")]
    [Space]
    [SerializeField] protected List<PuzzleKeyContainer> keyContainersList = new List<PuzzleKeyContainer>();

    private PuzzleGameUI _puzzleGameUI;
    private TimersManager _timersManager;
    private CurrentGameManager _currentGameManager;
    private DataPersistanceManager _dataPersistanceManager;

    public int EnvironmentIndex { get => environmentIndex; }
    public PuzzleCollectableItemsManager CollectableItemsManager { get => collectableItemsManager; }
    public PuzzleInputManager InputManager { get => inputManager; }

    private void Awake()
    {
        _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
        collectableItemsManager.CashComponents(this);
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
    private void Construct(TimersManager timersManager, CurrentGameManager currentGameManager, PuzzleGameUI puzzleGameUI, DataPersistanceManager dataPersistanceManager)
    {
        _timersManager = timersManager;
        _currentGameManager = currentGameManager;
        _puzzleGameUI = puzzleGameUI;
        _dataPersistanceManager = dataPersistanceManager;
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

    private void SubscribeOnEvents()
    {
        
    }

    private void UnsubscribeFromEvents()
    {
        
    }

    private IEnumerator StartGameCoroutine()
    {
        float currentCounterValue = startGameDelay;
        yield return new WaitForSeconds(startGameCoroutineDelay);

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

    public void LoadData(GameData data)
    {
        _puzzleGameUI.SetLevelLoadedData(data.puzzleGameLevelsDataList[environmentIndex]); 
        collectableItemsManager.LoadCollectedItemsData(data.puzzleGameLevelsDataList[environmentIndex].collectedItemsList, CollectedItemsDataLoaded_ExecuteReaction);
    }

    public void SaveData(GameData data)
    {
        List<int> collectedItemsIndexesList = new List<int>();

        for(int i = 0; i < collectableItemsManager.ItemsInInventoryList.Count; i++)
        {
            collectedItemsIndexesList.Add((int)collectableItemsManager.ItemsInInventoryList[i]);
        }

        data.puzzleGameLevelsDataList[environmentIndex].collectedItemsList = collectedItemsIndexesList;
    }

    public void CollectedItemsDataLoaded_ExecuteReaction()
    {
        for (int i = 0; i < keyContainersList.Count; i++)
        {
            keyContainersList[i].EnvironmentCollectedItemsDataLoaded(collectableItemsManager.ItemsInInventoryList);
        }
    }
}
