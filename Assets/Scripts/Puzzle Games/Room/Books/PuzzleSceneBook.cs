using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSceneBook : MonoBehaviour
{
    [Header("Sprites")]
    [Space]
    [SerializeField] private Sprite greenBookSprite;
    [SerializeField] private Sprite yellowBookSprite;
    [SerializeField] private Sprite blueBookSprite;

    private SpriteRenderer spriteRenderer;

    private Sprite startSprite;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startSprite = spriteRenderer.sprite;
    }

    public void ChangeSpriteToFinished(PuzzleGameKitchenBooks booksType)
    {
        if(booksType == PuzzleGameKitchenBooks.Green)
        {
            spriteRenderer.sprite = greenBookSprite;
        }
        else if (booksType == PuzzleGameKitchenBooks.Yellow)
        {
            spriteRenderer.sprite = yellowBookSprite;
        }
        else if (booksType == PuzzleGameKitchenBooks.Blue)
        {
            spriteRenderer.sprite = blueBookSprite;
        }
    }

    public void ChangeBookSpriteToStart()
    {
        spriteRenderer.sprite = startSprite;
    }
}
