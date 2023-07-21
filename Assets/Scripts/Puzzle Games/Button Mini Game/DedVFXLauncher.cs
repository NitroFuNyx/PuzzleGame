using UnityEngine;
using Zenject;

public class DedVFXLauncher : MonoBehaviour
{
    [SerializeField] private ParticleSystem LightningVFX;
    [SerializeField] private ParticleSystem KeyAppearVFX;

    private BoxCollider2D boxCollider;

    private AudioManager _audioManager;
    private CurrentGameManager _currentGameManager;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        boxCollider.enabled = false;
    }

    #region Zenject
    [Inject]
    private void Construct(AudioManager audioManager, CurrentGameManager currentGameManager)
    {
        _audioManager = audioManager;
        _currentGameManager = currentGameManager;
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
        boxCollider.enabled = true;
    }
    
    public void TurnOffCollider()
    {
        boxCollider.enabled = false;
    }
}
