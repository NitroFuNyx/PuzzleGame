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

    private PuzzleGamesEnvironmentsHolder _puzzleGamesEnvironmentsHolder;

    public int LockIndex { get => lockIndex; }

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

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriteRenderer.sprite = closedLocksSpritesList[lockIndex];
    }

    public void OpenLock()
    {
        spriteRenderer.sprite = openLocksSpritesList[lockIndex];
    }

    public void ResetLock()
    {

    }
}
