using UnityEngine;
using Zenject;

public class SafeOpener : MonoBehaviour
{
    [SerializeField] private Sprite openedDoor;
    [SerializeField] private Sprite closedDoor;

    [SerializeField] private SpriteRenderer spriteRenderer;
    private PuzzleGameUI _puzzleGameUI;

    [Inject]
    private void InjectDependencies(PuzzleGameUI puzzleGameUI)
    {
        _puzzleGameUI = puzzleGameUI;
    }

    
    private void Start()
    {
        
        _puzzleGameUI.OnOpenSafeGameFinished+=OpenSafe;    
    }

    private void OnDestroy()
    {
        _puzzleGameUI.OnOpenSafeGameFinished-=OpenSafe;    

    }

    public void OpenSafe()
    {
        spriteRenderer.sprite = openedDoor;
    }
    public void CloseSafe()
    {
        spriteRenderer.sprite = closedDoor;
    }
}
