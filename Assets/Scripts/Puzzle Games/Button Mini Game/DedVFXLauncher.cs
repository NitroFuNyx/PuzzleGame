using UnityEngine;
using Zenject;

public class DedVFXLauncher : MonoBehaviour
{
    [SerializeField] private ParticleSystem LightningVFX;
    [SerializeField] private ParticleSystem KeyAppearVFX;

    private AudioManager _audioManager;

    #region Zenject
    [Inject]
    private void Construct(AudioManager audioManager)
    {
        _audioManager = audioManager;
    }
    #endregion Zenject

    public void EmitLightning()
    {
        _audioManager.PlaySFXSound_OldManAppeared();
        LightningVFX.Stop();
        LightningVFX.Play();
    }
    public void EmitKeyAppearVFX()
    {
        KeyAppearVFX.Stop();
        KeyAppearVFX.Play();
    }
    
}
