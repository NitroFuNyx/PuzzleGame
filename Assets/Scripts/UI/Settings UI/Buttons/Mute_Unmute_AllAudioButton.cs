using UnityEngine;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;

public class Mute_Unmute_AllAudioButton : ButtonInteractionHandler
{
    [Header("Button Sprites")]
    [Space]
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;
    [Header("Scale Data")]
    [Space]
    [SerializeField] private Vector3 minScale = new Vector3(0.8f, 0.8f, 0.8f);
    [SerializeField] private float scaleDuration = 0.3f;

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
        transform.DOScale(minScale, scaleDuration).OnComplete(() =>
        {
            transform.DOScale(Vector3.one, scaleDuration);
        });
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
