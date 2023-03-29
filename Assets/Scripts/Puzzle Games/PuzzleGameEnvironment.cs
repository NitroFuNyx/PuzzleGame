using UnityEngine;
using System.Collections;
using Zenject;

public class PuzzleGameEnvironment : MonoBehaviour
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

    private PuzzleGameUI _puzzleGameUI;
    private TimersManager _timersManager;
    private CurrentGameManager _currentGameManager;

    public int EnvironmentIndex { get => environmentIndex; }
    public PuzzleCollectableItemsManager CollectableItemsManager { get => collectableItemsManager; }

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
    private void Construct(TimersManager timersManager, CurrentGameManager currentGameManager, PuzzleGameUI puzzleGameUI)
    {
        _timersManager = timersManager;
        _currentGameManager = currentGameManager;
        _puzzleGameUI = puzzleGameUI;
    }
    #endregion Zenject

    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
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
}
