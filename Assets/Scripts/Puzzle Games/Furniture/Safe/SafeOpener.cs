using UnityEngine;

public class SafeOpener : MonoBehaviour
{
    [SerializeField] private SafeButtonsHandler safeButtonsHandler;
    [SerializeField] private Sprite openedDoor;

    [SerializeField] private SpriteRenderer spriteRenderer;
    
    private void Start()
    {
        
        safeButtonsHandler._OnGameFinished+=OpenSafe;    
    }

    private void OnDestroy()
    {
        safeButtonsHandler._OnGameFinished-=OpenSafe;    

    }

    private void OpenSafe()
    {
        spriteRenderer.sprite = openedDoor;
        Debug.Log("Change sprite");
    }
}
