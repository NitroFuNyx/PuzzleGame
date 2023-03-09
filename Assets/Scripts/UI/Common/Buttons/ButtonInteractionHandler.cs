using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
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
    //[Header("Delays")]
    //[Space]
    //[SerializeField] private float activateButtonMethodDelay = 0.6f;

    private Button _button;

    protected Button ButtonComponent { get => _button; private set => _button = value; }

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

    protected IEnumerator ActivateButtonMethodCoroutine(Action DelayedButtonMethod)
    {
        yield return new WaitForSeconds(scaleDuration * 2);
        DelayedButtonMethod();
    }
}
