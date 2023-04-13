using UnityEngine;

public class PlayerAnimationsManager : MonoBehaviour
{
    [Header("Animators")]
    [Space]
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        animator.SetInteger(SkinAnimations.MiniGameCharacterIndex, 1);
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
