using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonInteractionHandler : MonoBehaviour
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

    public void SetButtonActive()
    {
        ButtonComponent.interactable = true;
    }

    public void SetButtonDisabled()
    {
        ButtonComponent.interactable = false;
    }

    public abstract void ButtonActivated();
}
