using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLock : MonoBehaviour
{
    [Header("Lock Type Data")]
    [Space]
    [SerializeField] private int lockIndex;
    [Header("Lock Sprites")]
    [Space]
    [SerializeField] private List<Sprite> closedLocksSpritesList = new List<Sprite>();
    [SerializeField] private List<Sprite> openLocksSpritesList = new List<Sprite>();

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriteRenderer.sprite = closedLocksSpritesList[lockIndex];
    }
}
