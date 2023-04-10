using UnityEngine;

public class WindowCharacterVFXManager : MonoBehaviour
{
    [Header("VFX")]
    [Space]
    [SerializeField] private ParticleSystem KeyAppearVFX;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void EmitLightning()
    {
        //LightningVFX.Stop();
        //LightningVFX.Play();
    }

    public void EmitKeyAppearVFX()
    {
        KeyAppearVFX.Stop();
        KeyAppearVFX.Play();
    }

    public void SetAnimationState_GetKey()
    {
        animator.SetTrigger(WindowCharacterAnimations.GetKey);
    }
}
