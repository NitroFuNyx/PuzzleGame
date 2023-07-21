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
        _audioManager.PlayVoicesAudio_OldManGivesKey();
        LightningVFX.Stop();
        LightningVFX.Play();
    }
    public void EmitKeyAppearVFX()
    {
        //_audioManager.PlayVoicesAudio_OldManGivesKey();
        KeyAppearVFX.Stop();
        KeyAppearVFX.Play();
    }
    
}
