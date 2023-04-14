using UnityEngine;
using Zenject;

public class PlayerComponentsManager : MonoBehaviour
{
    [Header("Positions")]
    [Space]
    [SerializeField] private Vector3 startPos;
    [Header("Internal References")]
    [Space]
    [SerializeField] private CharacterSkinSelector characterSkinSelector;

    private PlayerMoveManager moveManager;
    private PlayerAnimationsManager animationsManager;
    private PlayerCollisionManager collisionManager;

    private CurrentGameManager _currentGameManager;

    private void Awake()
    {
        CashComponents();
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
    private void Construct(CurrentGameManager currentGameManager)
    {
        _currentGameManager = currentGameManager;
    }
    #endregion Zenject

    public void ResetPlayer()
    {
        moveManager.ChangeCheckingInputState(false);
        animationsManager.SetAnimationState_StopWalking();
        collisionManager.ResetStates();
        transform.position = startPos;
    }

    private void CashComponents()
    {
        moveManager = GetComponent<PlayerMoveManager>();
        animationsManager = GetComponent<PlayerAnimationsManager>();
        collisionManager = GetComponent<PlayerCollisionManager>();
    }

    private void SubscribeOnEvents()
    {
        moveManager.OnCharacterStartMoving += MoveManager_ExecuteReaction_OnPlayerStartMoving;
        moveManager.OnCharacterStopMoving += MoveManager_ExecuteReaction_OnPlayerStopMoving;

        collisionManager.OnCharacterStunned += CollisionManager_ExecuteReaction_OnPlayerStunned;
        collisionManager.OnCharacterStunnedStateFinished += CollisionManager_ExecuteReaction_OnCharacterStunnedStateFinished;
    }

    private void UnsubscribeFromEvents()
    {
        moveManager.OnCharacterStartMoving -= MoveManager_ExecuteReaction_OnPlayerStartMoving;
        moveManager.OnCharacterStopMoving -= MoveManager_ExecuteReaction_OnPlayerStopMoving;

        collisionManager.OnCharacterStunned -= CollisionManager_ExecuteReaction_OnPlayerStunned;
        collisionManager.OnCharacterStunnedStateFinished -= CollisionManager_ExecuteReaction_OnCharacterStunnedStateFinished;
    }

    private void MoveManager_ExecuteReaction_OnPlayerStartMoving()
    {
        animationsManager.SetAnimationState_StartWalking();
    }

    private void MoveManager_ExecuteReaction_OnPlayerStopMoving()
    {
        animationsManager.SetAnimationState_StopWalking();
    }

    private void CollisionManager_ExecuteReaction_OnPlayerStunned()
    {
        moveManager.ChangeCheckingInputState(false);
        animationsManager.SetAnimationState_StopWalking();
    }

    private void CollisionManager_ExecuteReaction_OnCharacterStunnedStateFinished()
    {
        moveManager.ChangeCheckingInputState(true);
    }

    public void AnimationManager_ExecuteReaction_OnCharacterSkinAnimationUpdated()
    {
        characterSkinSelector.SetSkin(_currentGameManager.CurrentCharacter);
    }
}
