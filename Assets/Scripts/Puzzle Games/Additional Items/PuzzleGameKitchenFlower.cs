using UnityEngine;
using DG.Tweening;

public class PuzzleGameKitchenFlower : MonoBehaviour, Iinteractable
{
    [Header("Scale Data")]
    [Space]
    [SerializeField] private Vector3 scaleDelta = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField] private float scaleDuration = 1f;
    [SerializeField] private Ease scaleFunction;
    [SerializeField] private int scaleMaxCount = 4;

    private bool animationInProcess = false;

    private int scaleCounter = 0;

    public void Interact()
    {
        ScaleObject();
    }

    private void ScaleObject()
    {
        if(!animationInProcess)
        {
            animationInProcess = true;
            scaleCounter++;
            Vector3 scaleVector = transform.localScale + scaleDelta;
            transform.DOScale(scaleVector, scaleDuration).SetEase(scaleFunction).OnComplete(() =>
            {
                //if(scaleCounter < scaleMaxCount)
                //{
                    animationInProcess = false;
                //}
                //else
                //{

                //}
            });
        }
    }
}
