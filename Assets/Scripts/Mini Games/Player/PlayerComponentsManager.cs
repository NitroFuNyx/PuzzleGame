using UnityEngine;

public class PlayerComponentsManager : MonoBehaviour
{
    [Header("Positions")]
    [Space]
    [SerializeField] private Vector3 startPos;

    private PlayerMoveManager moveManager;
    private PlayerAnimationsManager animationsManager;
    private PlayerCollisionManager collisionManager;

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
}
