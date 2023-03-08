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
    [Header("Change Scale Data")]
    [Space]
    [SerializeField] private Vector3 punchScaleVector = new Vector3(0.2f, 0.2f, 0.2f);
    [SerializeField] private int scaleFrequency = 3;
    [SerializeField] private float changeScaleDuration = 1f;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        SetStartSettings();
    }

    public void ChangeAlphaToMax()
    {
        image.DOFade(1f, changeAlphaDuration);
        transform.DOPunchScale(punchScaleVector, changeScaleDuration, scaleFrequency);
    }

    public void SetStartSettings()
    {
        image.DOFade(minAlphaValue, changeAlphaInstantlyDuration);
    }
}
