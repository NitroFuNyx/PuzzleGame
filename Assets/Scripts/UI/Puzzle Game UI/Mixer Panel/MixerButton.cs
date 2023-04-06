using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MixerButton : ButtonInteractionHandler
{
    [Header("Progress Data")]
    [Space]
    [SerializeField] private float currentProgressValue = 0f;
    [Header("Images")]
    [Space]
    [SerializeField] private Image progressImage;

    private float startProgressValue = 0f;
    private float maxProgressValue = 1f;

    private float progressIncreaseDeltaValue = 0.05f;
    private float progresDecreaseDeltaValue = 0.0003f;

    private float hidePanelDelay = 0.4f;

    private bool gameInProgress = false;

    private Action _OnGameFinished;

    private void Start()
    {
        currentProgressValue = 0f;
        progressImage.fillAmount = 0f;
    }

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

        if(currentProgressValue >= maxProgressValue)
        {
            gameInProgress = false;
            StartCoroutine(HidePanelCoroutine());
        }
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
