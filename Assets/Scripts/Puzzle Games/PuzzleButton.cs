using UnityEngine;
using Zenject;

public class PuzzleButton : MonoBehaviour, Iinteractable
{
    [Header("Item Type")]
    [Space]
    [SerializeField] protected PuzzleGameKitchenMiniGames gameType;
  
    


    private bool buttonPressed = false;
    private PuzzleGameUI _puzzleGameUI;
    
    #region Zenject
    [Inject]
    private void Construct(PuzzleGameUI puzzleGameUI)
    {
        _puzzleGameUI = puzzleGameUI;
    }
    #endregion Zenject


    public void Interact()
    {
        Debug.Log($"Puzzle Button");
        buttonPressed = !buttonPressed;

        if(buttonPressed)
        {
            _puzzleGameUI.ShowMiniGamePanel(gameType);
        }
        
    }
}
