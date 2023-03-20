using UnityEngine;
using System.Collections;
using Zenject;

public class MiniGameEnvironment : MonoBehaviour
{
    [Header("Environment Data")]
    [Space]
    [SerializeField] private int environmentIndex;
    [Header("Time Data")]
    [Space]
    [SerializeField] private float startGameDelay = 3f;
    [SerializeField] private float startGameCoroutineDelay = 0.5f;
    [SerializeField] private float timeForLevel = 180f;
    [Header("Internal References")]
    [Space]
    [SerializeField] private PlayerMoveManager playerMoveManager;
    [SerializeField] private KitchenMiniGameSpawnManager kitchenMiniGameSpawnManager;

    private MiniGameUI _miniGameUI;
    private TimersManager _timersManager;
    private CurrentGameManager _currentGameManager;
    private ResourcesManager _resourcesManager;

    private PlayerCollisionManager playerCollisionManager;
    private PlayerComponentsManager playerComponentsManager;

    public int EnvironmentIndex { get => environmentIndex; }

    private void Awake()
    {
        playerCollisionManager = playerMoveManager.GetComponent<PlayerCollisionManager>();
        playerComponentsManager = playerMoveManager.GetComponent<PlayerComponentsManager>();
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
    private void Construct(MiniGameUI miniGameUI, TimersManager timersManager, CurrentGameManager currentGameManager, ResourcesManager resourcesManager)
    {
        _miniGameUI = miniGameUI;
        _timersManager = timersManager;
        _currentGameManager = currentGameManager;
        _resourcesManager = resourcesManager;
    }
    #endregion Zenject

    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    public void ResetEnvironment()
    {
        playerComponentsManager.ResetPlayer();
    }

    private void SubscribeOnEvents()
    {
        playerCollisionManager.OnAdditionalTimeBonusCollected += OnPlayerCollectedBonus_AdditionaTime_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        playerCollisionManager.OnAdditionalTimeBonusCollected -= OnPlayerCollectedBonus_AdditionaTime_ExecuteReaction;
    }

    private void TimerFinished_ExecuteReaction()
    {
        playerMoveManager.ChangeCheckingInputState(false);
        _resourcesManager.SaveLevelData();
        StartCoroutine(TimerFinished_ExecuteReactionCoroutine());
    }

    private void OnPlayerCollectedBonus_AdditionaTime_ExecuteReaction(float additionalTime)
    {
        _miniGameUI.ShowBonusTime(additionalTime);
    }

    private IEnumerator StartGameCoroutine()
    {
        float currentCounterValue = startGameDelay;
        yield return new WaitForSeconds(startGameCoroutineDelay);

        while(currentCounterValue > 0)
        {
            _miniGameUI.UpdateStartGameDelayTimerText(currentCounterValue);
            currentCounterValue--;
            yield return new WaitForSeconds(1f);
            _miniGameUI.UpdateStartGameDelayTimerText(currentCounterValue);
        }

        yield return new WaitForSeconds(startGameCoroutineDelay);
        _miniGameUI.HideDelayTimerText();
        _miniGameUI.StartCurrentGameTimer(timeForLevel, TimerFinished_ExecuteReaction);
        playerMoveManager.ChangeCheckingInputState(true);
        playerCollisionManager.ChangeState_CanCollectItems(true);
        kitchenMiniGameSpawnManager.StartSpawningItems();
    }

    private IEnumerator TimerFinished_ExecuteReactionCoroutine()
    {
        yield return null;
        _currentGameManager.SetGameFinisheData();
        kitchenMiniGameSpawnManager.StopSpawning();
    }
}
