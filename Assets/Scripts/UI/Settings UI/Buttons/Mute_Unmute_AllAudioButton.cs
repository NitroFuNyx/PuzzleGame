using UnityEngine;
using Zenject;

public class Mute_Unmute_AllAudioButton : ButtonInteractionHandler
{
    [Header("Button Sprites")]
    [Space]
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;

    private AudioManager _audioManager;

    private bool muted = false;

    private void Start()
    {
        _audioManager.OnAudioMuteStateChanged += AudioMuteStateChanged_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _audioManager.OnAudioMuteStateChanged -= AudioMuteStateChanged_ExecuteReaction;
    }

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
        ShowAnimation_ButtonPressed();
    }

    private void ChangePanelSprite()
    {
        if(muted)
        {
            buttonImage.sprite = soundOffSprite;
        }
        else
        {
            buttonImage.sprite = soundOnSprite;
        }
    }

    private void AudioMuteStateChanged_ExecuteReaction(bool audioMuted)
    {
        muted = audioMuted;
        ChangePanelSprite();
    }
}
