using UnityEngine;
using Zenject;
using DG.Tweening;

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
        _audioManager.ChangeMuteState(muted);
        transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.3f).OnComplete(() =>
        {
            transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f);
        });
    }
}
