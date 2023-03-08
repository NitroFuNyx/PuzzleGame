using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class ButtonInteractionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button _button;

    public Button ButtonComponent { get => _button; private set => _button = value; }

    private void Awake()
    {
        if (TryGetComponent(out Button button))
        {
            ButtonComponent = button;
            ButtonComponent.onClick.AddListener(ButtonActivated);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ProcessEvent_PointerEnter(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ProcessEvent_PointerExit(eventData);
    }

    public void SetButtonActive()
    {
        ButtonComponent.interactable = true;
    }

    public void SetButtonDisabled()
    {
        ButtonComponent.interactable = false;
    }

    public abstract void ProcessEvent_PointerEnter(PointerEventData eventData);

    public abstract void ProcessEvent_PointerExit(PointerEventData eventData);

    public abstract void ButtonActivated();
}
