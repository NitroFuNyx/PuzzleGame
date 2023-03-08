using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainLoaderProgressPanel : MonoBehaviour
{
    [Header("Loader Progress Images")]
    [Space]
    [SerializeField] private List<MainLoaderProgressImage> loaderImagesList = new List<MainLoaderProgressImage>();
    [Header("Delays")]
    [Space]
    [SerializeField] private float loaderProgressMinDelay = 0.3f;
    [SerializeField] private float loaderProgressMaxDelay = 1f;

    #region Events Declaration
    public event Action OnLoaderAnimationFinished;
    #endregion Events Declatration

    public void ShowProgress()
    {
        StartCoroutine(ShowProgressCoroutine());
    }

    public void ResetImagesState()
    {
        for(int i = 0; i < loaderImagesList.Count; i++)
        {
            loaderImagesList[i].SetStartSettings();
        }
    }

    private IEnumerator ShowProgressCoroutine()
    {
        for (int i = 0; i < loaderImagesList.Count; i++)
        {
            float delay = UnityEngine.Random.Range(loaderProgressMinDelay, loaderProgressMaxDelay);
            yield return new WaitForSeconds(delay);
            loaderImagesList[i].ChangeAlphaToMax();
        }

        OnLoaderAnimationFinished?.Invoke();
    }
}
