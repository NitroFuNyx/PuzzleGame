using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MixerButton : ButtonInteractionHandler
{
    [Header("Progress Data")]
    [Space]
    [SerializeField] private float currentProgressValue = 0f;
    [Header("Images")]
    [Space]
    [SerializeField] private Image progressImage;
    [SerializeField] private Image mixerTool;

    [SerializeField] private Sprite ToolState1;
    [SerializeField] private Sprite ToolState2;

    private AudioManager _audioManager;

    private float startProgressValue = 0f;
    private float maxProgressValue = 1f;

    private float progressIncreaseDeltaValue = 0.05f;
    private float progresDecreaseDeltaValue = 0.0003f;

    private float hidePanelDelay = 0.4f;

    private bool gameInProgress = false;
    private bool IsStandartToolSprite = true;

    private Action _OnGameFinished;

    private void Start()
    {
        ResetGame();
    }

    #region Zenject
    [Inject]
    private void Construct(AudioManager audioManager)
    {
        _audioManager = audioManager;
    }
    #endregion Zenject

    public void StartGame(Action OnGameFinished)
    {
        gameInProgress = true;
        currentProgressValue = startProgressValue;
        _OnGameFinished = OnGameFinished;
        StartCoroutine(DecreaseProgressCoroutine());
    }

    public override void ButtonActivated()
    {
        currentProgressValue += progressIncreaseDeltaValue;
        progressImage.fillAmount = currentProgressValue;
        _audioManager.PlaySFXSound_PressMixerButton();
        if (IsStandartToolSprite)
            mixerTool.sprite = ToolState2;
        else mixerTool.sprite = ToolState1;
        IsStandartToolSprite = !IsStandartToolSprite;
        if(currentProgressValue >= maxProgressValue)
        {
            gameInProgress = false;
            _audioManager.StopSFXAudio();
            StartCoroutine(HidePanelCoroutine());
        }
    }

    public void ResetGame()
    {
        gameInProgress = false;
        currentProgressValue = 0f;
        progressImage.fillAmount = 0f;
        StopAllCoroutines();
    }

    private IEnumerator DecreaseProgressCoroutine()
    {
        while (gameInProgress)
        {
            if (currentProgressValue > startProgressValue)
            {
                currentProgressValue -= progresDecreaseDeltaValue;
                if (currentProgressValue < 0f)
                {
                    currentProgressValue = 0f;
                }
                progressImage.fillAmount = currentProgressValue;
                yield return null;
            }
            yield return null;
        }
    }

    private IEnumerator HidePanelCoroutine()
    {
        yield return new WaitForSeconds(hidePanelDelay);
        _OnGameFinished?.Invoke();
    }
}
