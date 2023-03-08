using UnityEngine;
using Zenject;

public class Mute_Unmute_AllAudioButton : ButtonInteractionHandler
{
    private AudioManager _audioManager;

    private bool muted = false;

    #region Zenject
    [Inject]
    private void Construct(AudioManager audioManager)
    {
        _audioManager = audioManager;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        muted = !muted;
        Debug.Log($"{muted}");
        _audioManager.ChangeMuteState(muted);
    }
}
