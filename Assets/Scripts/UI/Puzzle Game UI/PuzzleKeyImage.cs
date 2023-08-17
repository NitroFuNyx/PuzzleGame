using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PuzzleKeyImage : MonoBehaviour
{
    [Header("VFX Data")]
    [Space]
    [SerializeField] private ParticleSystem keyImageDestructionVFX;
    [Header("Durations")]
    [Space]
    [SerializeField] private float moveToInventoryPanelDuration = 1f;
    [SerializeField] private float changeAlphaDuration = 0.1f;
    [Header("Delays")]
    [Space]
    [SerializeField] private float destroyObjectDelay = 2f;

    private PuzzleGameUI _puzzleGameUI;

    private Image keyImage;
    private PuzzleGameCollectableItems itemType;

    private void Awake()
    {
        keyImage = GetComponent<Image>();
    }

    public void MoveToInventoryPanel(Vector3 inventoryPanelPos, Sprite keySprite, PuzzleGameCollectableItems item, PuzzleGameUI puzzleGameUI/*, Action OnImageHidden*/)
    {
        keyImage.sprite = keySprite;
        itemType = item;
        _puzzleGameUI = puzzleGameUI;

        transform.DOMove(inventoryPanelPos, moveToInventoryPanelDuration).OnComplete(() =>
        {
            keyImageDestructionVFX.Play();
            keyImage.DOFade(0f, changeAlphaDuration);
            StartCoroutine(DestroyObjectCoroutine(/*OnImageHidden*/));
        });
    }

    private IEnumerator DestroyObjectCoroutine(/*Action OnImageHidden*/)
    {
        yield return new WaitForSeconds(destroyObjectDelay / 2);
        //OnImageHidden?.Invoke();
        _puzzleGameUI.ShowItemImageInInventoryPanel(keyImage.sprite, itemType);
        yield return new WaitForSeconds(destroyObjectDelay / 2);
        Destroy(gameObject);
    }
}
