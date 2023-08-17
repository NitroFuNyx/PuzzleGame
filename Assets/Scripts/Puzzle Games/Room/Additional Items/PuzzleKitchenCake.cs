using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleKitchenCake : MonoBehaviour
{
    [Header("VFX")]
    [Space]
    [SerializeField] private List<ParticleSystem> vfxList = new List<ParticleSystem>();
    [Header("Delays")]
    [Space]
    [SerializeField] private float vfxDelay = 0.5f;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAnimationState_Jump()
    {
        animator.SetTrigger(CakeAnimations.Jump);
        StartCoroutine(PlayVFXCoroutine());
    }

    private IEnumerator PlayVFXCoroutine()
    {
        yield return new WaitForSeconds(vfxDelay);

        for(int i = 0; i < vfxList.Count; i++)
        {
            vfxList[i].Play();
        }
    }
}
