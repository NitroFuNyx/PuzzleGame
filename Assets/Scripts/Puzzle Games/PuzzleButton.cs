using UnityEngine;
using Zenject;

public class PuzzleButton : MonoBehaviour, Iinteractable
{
    [Header("Item Type")]
    [Space]
    [SerializeField] protected PuzzleGameKitchenMiniGames gameType;
    [Header("Sprites")]
    [Space]
    [SerializeField] private Sprite buttonPressedSprite;
    [SerializeField] private Sprite buttonReleasedSprite;
    

    private SpriteRenderer spriteRenderer;

    private bool buttonPressed = false;
    private PuzzleGameUI _puzzleGameUI;
    
    #region Zenject
    [Inject]
    private void Construct(PuzzleGameUI puzzleGameUI)
    {
        _puzzleGameUI = puzzleGameUI;
    }
    #endregion Zenject

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
            _puzzleGameUI.ShowMiniGamePanel(gameType);
        }
        else
        {
            /*spriteRenderer.sprite = buttonPressedSprite;
            spriteRenderer.sprite = buttonReleasedSprite;*/
        }
    }
}
