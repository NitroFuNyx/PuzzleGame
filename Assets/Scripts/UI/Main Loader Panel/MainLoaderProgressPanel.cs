using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLoaderProgressPanel : MonoBehaviour
{
    [Header("Loader Progress Images")]
    [Space]
    [SerializeField] private List<MainLoaderProgressImage> loaderImagesList = new List<MainLoaderProgressImage>();
    [Header("Delays")]
    [Space]
    [SerializeField] private float loaderProgressMinDelay = 0.3f;
    [SerializeField] private float loaderProgressMaxDelay = 1f;

    [ContextMenu("Show Progress")]
    public void ShowProgress()
    {
        StartCoroutine(ShowProgressCoroutine());
    }

    private IEnumerator ShowProgressCoroutine()
    {
        for (int i = 0; i < loaderImagesList.Count; i++)
        {
            float delay = Random.Range(loaderProgressMinDelay, loaderProgressMaxDelay);
            yield return new WaitForSeconds(delay);
            loaderImagesList[i].ChangeAlphaToMax();
        }
    }
}
