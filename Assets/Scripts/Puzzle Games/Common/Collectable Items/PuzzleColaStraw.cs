using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class PuzzleColaStraw : PuzzleCollectableItem
{
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
    private PuzzleColaGlass colaGlass;

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

    public override void InteractOnTouch()
    {
        if (!collected)
        {
            collected = true;
            Debug.Log($"Save {Item}");
            MoveToInventory();
        }
    }

    public void CashComponents(PuzzleColaGlass puzzleColaGlass)
    {
        colaGlass = puzzleColaGlass;
    }

    public void ResetItem()
    {
        collected = false;
    }

    private void MoveToInventory()
    {
        Vector3 startPos = transform.position;
        transform.DOJump(startPos, jumpPower, 1, jumpDuration);
        transform.DOPunchRotation(rotationPunchVector, punchDuration, punchFreequency).OnComplete(() =>
        {
            _puzzleGameUI.MoveItemToInventoryBar(spriteRenderer, Item);
            _puzzleGamesEnvironmentsHolder.CurrentlyActiveGame.CollectableItemsManager.AddItemToInventory(this);
            gameObject.SetActive(false);
        });
    }
}
