using UnityEngine;

public class PanelActivationManager : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    public CanvasGroup _CanvasGroup { get => canvasGroup; private set => canvasGroup = value; }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ShowPanel()
    {
        SetCanvasActivationState(true);
    }

    public void HidePanel()
    {
        SetCanvasActivationState(false);
    }

    public void SetCanvasActivationState(bool isActive)
    {
        if (isActive)
        {
            canvasGroup.alpha = 1f;
        }
        else
        {
            canvasGroup.alpha = 0f;
        }

        canvasGroup.interactable = isActive;
        canvasGroup.blocksRaycasts = isActive;
    }
}
