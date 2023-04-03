using UnityEngine;

public class PuzzleButton : MonoBehaviour, Iinteractable
{
    [Header("Sprites")]
    [Space]
    [SerializeField] private Sprite buttonPressedSprite;
    [SerializeField] private Sprite buttonReleasedSprite;

    private SpriteRenderer spriteRenderer;

    private bool buttonPressed = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Interact()
    {
        Debug.Log($"Puzzle Button");
        buttonPressed = !buttonPressed;

        if(buttonPressed)
        {
            spriteRenderer.sprite = buttonPressedSprite;
        }
        else
        {
            spriteRenderer.sprite = buttonReleasedSprite;
        }
    }
}
