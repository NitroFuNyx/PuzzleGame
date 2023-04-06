using UnityEngine;

public class DedVFXLauncher : MonoBehaviour
{
    [SerializeField] private ParticleSystem LightningVFX;
    [SerializeField] private ParticleSystem KeyAppearVFX;

    public void EmitLightning()
    {
        LightningVFX.Stop();
        LightningVFX.Play();
    }
    public void EmitKeyAppearVFX()
    {
        KeyAppearVFX.Stop();
        KeyAppearVFX.Play();
    }
    
}
