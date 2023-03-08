using UnityEngine;

public class PanelActivationManager : MonoBehaviour
{
    private CanvasGroup canvasGroup;

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
