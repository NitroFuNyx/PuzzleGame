using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class PuzzleKey : PuzzleGameItemInteractionHandler
{
    [Header("Key Data")]
    [Space]
    [SerializeField] private int keyIndex = 0;
    [Header("Rotate Data")]
    [Space]
    [SerializeField] private Vector3 rotationPunchVector = new Vector3(1f, 1f, 1f);
    [SerializeField] private float punchDuration = 1f;
    [SerializeField] private int punchFreequency = 3;
    [Header("Jump Data")]
    [Space]
    [SerializeField] private float jumpDelta = 1f;
    [SerializeField] private float jumpPower = 1f;
    [SerializeField] private float jumpDuration = 1f;

    private PuzzleGameUI _puzzleGameUI;

    private SpriteRenderer spriteRenderer;

    private bool collected = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    #region Zenject
    [Inject]
    private void Construct(PuzzleGameUI puzzleGameUI)
    {
        _puzzleGameUI = puzzleGameUI;
    }
    #endregion Zenject

    public override void Interact()
    {
        if(!collected)
        {
            collected = true;
            Debug.Log($"Save Key {keyIndex}");
            MoveToInventory();
        }
    }

    private void MoveToInventory()
    {
        Vector3 startPos = transform.position;
        transform.DOJump(startPos, jumpPower, 1, jumpDuration);
        transform.DOPunchRotation(rotationPunchVector, punchDuration, punchFreequency).OnComplete(() =>
        {
            _puzzleGameUI.MoveKeyToInventoryBar(spriteRenderer);
            gameObject.SetActive(false);
        });
    }
}
