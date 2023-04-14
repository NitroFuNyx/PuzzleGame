using UnityEngine;
using System;

public class PlayerAnimationsManager : MonoBehaviour
{
    [Header("Animators")]
    [Space]
    [SerializeField] private Animator animator;
    [Header("Internal References")]
    [Space]
    [SerializeField] private PlayerComponentsManager playerComponentsManager;

    private void OnEnable()
    {
        animator.SetInteger(SkinAnimations.MiniGameCharacterIndex, 1);
        playerComponentsManager.AnimationManager_ExecuteReaction_OnCharacterSkinAnimationUpdated();
    }

    public void SetAnimationState_StartWalking()
    {
        animator.SetTrigger(CharacterMoveAnimations.StartWalking);
    }

    public void SetAnimationState_StopWalking()
    {
        animator.SetTrigger(CharacterMoveAnimations.StopWalking);
    }
}
