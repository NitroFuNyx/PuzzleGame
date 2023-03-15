using UnityEngine;

public class PlayerAnimationsManager : MonoBehaviour
{
    [Header("Animators")]
    [Space]
    [SerializeField] private Animator animator;

    public void SetAnimationState_StartWalking()
    {
        animator.SetTrigger(CharacterMoveAnimations.StartWalking);
    }

    public void SetAnimationState_StopWalking()
    {
        animator.SetTrigger(CharacterMoveAnimations.StopWalking);
    }
}
