using UnityEngine;
using DG.Tweening;
using System.Collections;

public class MainScreenUI_ActionTextIdleAnimator : MonoBehaviour
{
    [Header("Scale Data")]
    [Space]
    [SerializeField] private Vector3 punchScale = new Vector3(0.3f, 0.3f, 0.3f);
    [SerializeField] private int scaleFreequency = 3;
    [SerializeField] private float scaleDuration = 1f;
    [SerializeField] private Ease ease;

    public void StartScaleAnimation()
    {
        transform.DOPunchScale(punchScale, scaleDuration, scaleFreequency).SetLoops(-1).SetEase(ease);
    }

    public void StopScaleAnimation()
    {
        transform.DOKill();
        transform.DOScale(Vector3.one, scaleDuration);
    }

    [ContextMenu("New")]
    public void Reset()
    {
        transform.DOKill();
        StartCoroutine(ResetCoroutine());
    }

    private IEnumerator ResetCoroutine()
    {
        yield return null;
        transform.localScale = Vector3.one;
        yield return null;
        transform.DOPunchScale(punchScale, scaleDuration, scaleFreequency).SetLoops(-1).SetEase(ease);
    }
}
