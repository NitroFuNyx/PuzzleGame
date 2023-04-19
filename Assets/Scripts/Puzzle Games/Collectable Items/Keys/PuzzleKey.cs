using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class PuzzleKey : PuzzleCollectableItem
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
    [Header("VFX")]
    [Space]
    [SerializeField] private ParticleSystem keyVFX;
    [Header("Sprites")]
    [Space]
    [SerializeField] private List<Sprite> keysSprites = new List<Sprite>();

    private PuzzleGameUI _puzzleGameUI;

    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;

    private Vector3 startPos = new Vector3();
    private Quaternion startRot;

    private bool collected = false;

    private int keyAppearedIndex = 0;

    public int KeyIndex { get => keyIndex; private set => keyIndex = value; }

    #region Events Declaration
    public event Action OnKeyCollected;
    #endregion Events Declaration

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        keyIndex = (int)Item;
        SetKeySprite();
        startPos = transform.position;
        startRot = transform.rotation;

        if(TryGetComponent(out Rigidbody2D rigidbody2D))
        {
            rb = rigidbody2D;
            rb.simulated = false;
        }
    }

    private void OnEnable()
    {
        if(keyVFX)
        {
            keyVFX.Play();
            keyAppearedIndex++;
            if (keyAppearedIndex > 1)
            {
                _audioManager.PlaySFXSound_KeyAppeared();
            }
        }
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
        if(!collected)
        {
            collected = true;
            _puzzleGamesEnvironmentsHolder.CurrentlyActiveGame.CluesManager.KeyCollected_ExecuteReaction(keyIndex);
            OnKeyCollected?.Invoke();
            Debug.Log($"Save {Item}");
            MoveToInventory();
        }
    }

    public void ChangeKeySimulattionState(bool isSimulated)
    {
        if(rb)
        {
            rb.simulated = isSimulated;
        }
    }

    public void ResetKey()
    {
        collected = false;
        ChangeKeySimulattionState(false);
        transform.position = startPos;
        transform.rotation = startRot;
    }

    private void MoveToInventory()
    {
        Vector3 startPos = transform.position;
        if(rb)
        {
            rb.simulated = false;
        }
        transform.DOJump(startPos, jumpPower, 1, jumpDuration);
        transform.DOPunchRotation(rotationPunchVector, punchDuration, punchFreequency).OnComplete(() =>
        {
            _puzzleGameUI.MoveKeyToInventoryBar(spriteRenderer, Item);
            _puzzleGamesEnvironmentsHolder.CurrentlyActiveGame.CollectableItemsManager.AddItemToInventory(this);
            gameObject.SetActive(false);
        });
    }

    private void SetKeySprite()
    {
        spriteRenderer.sprite = keysSprites[keyIndex];
    }
}
