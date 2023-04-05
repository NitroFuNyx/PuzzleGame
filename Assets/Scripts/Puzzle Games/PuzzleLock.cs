using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PuzzleLock : MonoBehaviour, Iinteractable
{
    [Header("Lock Type Data")]
    [Space]
    [SerializeField] private int lockIndex;
    [Header("Lock Sprites")]
    [Space]
    [SerializeField] private List<Sprite> closedLocksSpritesList = new List<Sprite>();
    [SerializeField] private List<Sprite> openLocksSpritesList = new List<Sprite>();

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private PuzzleGamesEnvironmentsHolder _puzzleGamesEnvironmentsHolder;

    private Vector3 startPos = new Vector3();

    public int LockIndex { get => lockIndex; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        startPos = transform.localPosition;
    }

    private void Start()
    {
        spriteRenderer.sprite = closedLocksSpritesList[lockIndex];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == Layers.PuzzleBottomBorderCollider)
        {
            rb.gravityScale = 0f;
            rb.simulated = false;
            spriteRenderer.sprite = closedLocksSpritesList[lockIndex];
            gameObject.SetActive(false);
            transform.localPosition = startPos;
        }
    }

    #region Zenject
    [Inject]
    private void Construct(PuzzleGamesEnvironmentsHolder puzzleGamesEnvironmentsHolder)
    {
        _puzzleGamesEnvironmentsHolder = puzzleGamesEnvironmentsHolder;
    }
    #endregion Zenject

    public void Interact()
    {
        _puzzleGamesEnvironmentsHolder.CurrentlyActiveGame.LockSelect(this);
    }

    public void OpenLock()
    {
        spriteRenderer.sprite = openLocksSpritesList[lockIndex];
        rb.gravityScale = 1f;
    }

    public void ResetLock()
    {

    }
}
