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
        ChangePanelSprite();
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
}
