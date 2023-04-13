using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleKitchenCake : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAnimationState_Jump()
    {
        animator.SetTrigger(CakeAnimations.Jump);
    }
}
