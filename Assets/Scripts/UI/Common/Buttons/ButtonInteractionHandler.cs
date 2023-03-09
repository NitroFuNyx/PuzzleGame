using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public abstract class ButtonInteractionHandler : MonoBehaviour
{
    [Header("Images")]
    [Space]
    [SerializeField] protected Image buttonImage;
    [Header("Scale Data")]
    [Space]
    [SerializeField] private Vector3 minScale = new Vector3(0.8f, 0.8f, 0.8f);
    [SerializeField] private float scaleDuration = 0.3f;

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

    public void ShowAnimation_ButtonPressed()
    {
        transform.DOScale(minScale, scaleDuration).OnComplete(() =>
        {
            transform.DOScale(Vector3.one, scaleDuration);
        });
    }

    public abstract void ButtonActivated();
}
