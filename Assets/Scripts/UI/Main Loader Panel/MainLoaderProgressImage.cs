using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainLoaderProgressImage : MonoBehaviour
{
    [Header("Change Alpha Data")]
    [Space]
    [SerializeField] private float minAlphaValue = 0.3f;
    [SerializeField] private float changeAlphaDuration = 1f;
    [SerializeField] private float changeAlphaInstantlyDuration = 0.01f;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        SetStartSettings();
    }

    public void ChangeAlphaToMax()
    {
        image.DOFade(1f, changeAlphaDuration);
        transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 1f, 3);
    }

    private void SetStartSettings()
    {
        image.DOFade(minAlphaValue, changeAlphaInstantlyDuration);
    }
}
