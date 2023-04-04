using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PuzzleGameKitchenFlower : MonoBehaviour, Iinteractable
{
    [Header("Scale Data")]
    [Space]
    [SerializeField] private Vector3 scaleDelta = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField] private float scaleDuration = 1f;
    [SerializeField] private Ease scaleFunction;
    [SerializeField] private int scaleMaxCount = 4;
    [Header("Jump Data")]
    [Space]
    [SerializeField] private Vector3 jumpDeltaVector = new Vector3(2f, 0f, 0f);
    [SerializeField] private float jumpDuration = 1f;
    [SerializeField] private float jumpPower = 1f;
    [Header("Keys")]
    [Space]
    [SerializeField] private PuzzleKey key;

    private bool animationInProcess = false;

    private int scaleCounter = 0;

    private void Start()
    {
        key.gameObject.SetActive(false);
    }

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

            if (scaleCounter < scaleMaxCount)
            {
                transform.DOScale(scaleVector, scaleDuration).SetEase(scaleFunction).OnComplete(() =>
                {
                    animationInProcess = false;
                });
                
            }
            else
            {
                transform.DOScale(scaleVector, scaleDuration).SetEase(scaleFunction);
                StartCoroutine(JumpCoroutine(scaleDuration / 2f));
            }
        }
    }

    private IEnumerator JumpCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.DOJump(transform.position + jumpDeltaVector, jumpPower, 1, jumpDuration);
        key.gameObject.SetActive(true);
    }
}
