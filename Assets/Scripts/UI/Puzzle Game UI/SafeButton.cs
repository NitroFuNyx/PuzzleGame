using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class SafeButton : MonoBehaviour
{
    [SerializeField] private string safeButtonNumber;
    [SerializeField] private Button button;
    [SerializeField] private SafeButtonsHandler safeButtonsHandler;

    public string SafeButtonNumber => safeButtonNumber;
    public event Action<string> ButtonPressed;

    private AudioManager _audioManager;

    #region Zenject
    [Inject]
    private void Construct(AudioManager audioManager)
    {
        _audioManager = audioManager;
    }
    #endregion Zenject

    private void Start()
    {
        button.onClick.AddListener(OnButtonPressed);
    }

    private  void OnButtonPressed()
    {
        ButtonPressed?.Invoke(safeButtonNumber);
        _audioManager.PlaySFXSound_PressSafeButton();
    }
}
