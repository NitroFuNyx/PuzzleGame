using UnityEngine;
using DG.Tweening;

public class PuzzleGamePainting : MonoBehaviour, Iinteractable
{
    [Header("Item Data")]
    [Space]
    [SerializeField] private PuzzleGameKitchenPzintings paintingType;
    [Header("Rotation")]
    [Space]
    [SerializeField] private Vector3 correctRotationVector = new Vector3(1f, 1f, 1f);
    [SerializeField] private float rotateDuration = 1f;

    private PuzzleGamePaintingsHolder paintingsHolder;

    private Quaternion startRotation;

    private bool isInCorrectPos = false;
    private bool animationInProccess = false;
    private bool canRotate = true;

    public bool IsInCorrectPos { get => isInCorrectPos; private set => isInCorrectPos = value; }

    private void Start()
    {
        startRotation = transform.rotation;
    }

    public void Interact()
    {
        if(canRotate)
        {
            isInCorrectPos = !isInCorrectPos;

            if (isInCorrectPos)
            {
                Rotate(correctRotationVector);
                paintingsHolder.PaintingRotated_ExecuteReaction();
            }
            else
            {
                Rotate(Vector3.zero);
            }
        }    
    }

    public void CashComponents(PuzzleGamePaintingsHolder puzzleGamePaintingsHolder)
    {
        paintingsHolder = puzzleGamePaintingsHolder;
    }

    public void ResetPainting()
    {
        isInCorrectPos = false;
        canRotate = true;
        transform.rotation = startRotation;
    }

    public void RotatePaintingAfterGettingKey()
    {
        isInCorrectPos = false;
        canRotate = false;
        Rotate(Vector3.zero);
    }

    public void RotateToCorrectPos()
    {
        transform.rotation = correctRotationVector;
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
