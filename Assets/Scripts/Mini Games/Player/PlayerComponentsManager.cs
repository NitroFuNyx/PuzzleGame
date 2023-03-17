using UnityEngine;

public class PlayerComponentsManager : MonoBehaviour
{
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
    }

    private void UnsubscribeFromEvents()
    {
        moveManager.OnCharacterStartMoving -= MoveManager_ExecuteReaction_OnPlayerStartMoving;
        moveManager.OnCharacterStopMoving -= MoveManager_ExecuteReaction_OnPlayerStopMoving;
    }

    private void MoveManager_ExecuteReaction_OnPlayerStartMoving()
    {
        animationsManager.SetAnimationState_StartWalking();
    }

    private void MoveManager_ExecuteReaction_OnPlayerStopMoving()
    {
        animationsManager.SetAnimationState_StopWalking();
    }
}
