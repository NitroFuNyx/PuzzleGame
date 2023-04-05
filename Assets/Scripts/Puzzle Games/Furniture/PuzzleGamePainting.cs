using UnityEngine;
using DG.Tweening;

public class PuzzleGamePainting : MonoBehaviour, Iinteractable
{
    [Header("Item Data")]
    [Space]
    [SerializeField] private PuzzleGameKitchenPzintings paintingType;
    [Header("Rotation")]
    [Space]
    [SerializeField] private Vector3 incorrectRotationVector = new Vector3(1f, 1f, 1f);
    [SerializeField] private float rotateDuration = 1f;

    private PuzzleGamePaintingsHolder paintingsHolder;

    private bool isInCorrectPos = false;
    private bool animationInProccess = false;

    public bool IsInCorrectPos { get => isInCorrectPos; private set => isInCorrectPos = value; }

    public void Interact()
    {
        Debug.Log($"{gameObject}");
        isInCorrectPos = !isInCorrectPos;

        if(isInCorrectPos)
        {
            Rotate(Vector3.zero);
            paintingsHolder.PaintingRotated_ExecuteReaction();
        }
        else
        {
            Rotate(incorrectRotationVector);
        }
    }

    public void CashComponents(PuzzleGamePaintingsHolder puzzleGamePaintingsHolder)
    {
        paintingsHolder = puzzleGamePaintingsHolder;
    }

    private void Rotate(Vector3 rotationVector)
    {
        animationInProccess = true;
        transform.DORotate(rotationVector, rotateDuration).SetEase(Ease.OutBack).OnComplete(() =>
        {
            animationInProccess = false;
        });
    }
}
