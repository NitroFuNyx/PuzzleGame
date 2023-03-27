using UnityEngine;
using DG.Tweening;

public class PuzzleGameItemStandart : PuzzleGameItemInteractionHandler
{
    [Header("Scale Data")]
    [Space]
    [SerializeField] private Vector3 scalePunchVector = new Vector3(0.2f, 0.2f, 0.2f);
    [SerializeField] private int scaleFreequency = 3;
    [SerializeField] private float scaleDuration = 0.5f;

    public override void Interact()
    {
        transform.DOPunchScale(scalePunchVector, scaleDuration, scaleFreequency);
    }

    [ContextMenu("Scale")]
    public void Scale()
    {
        transform.DOPunchScale(scalePunchVector, scaleDuration, scaleFreequency);
    }
}
