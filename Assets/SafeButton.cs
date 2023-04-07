using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SafeButton : MonoBehaviour
{
    [SerializeField] private string safeButtonNumber;
    [SerializeField] private Button button;
    [SerializeField] private SafeButtonsHandler safeButtonsHandler;
    public string SafeButtonNumber => safeButtonNumber;
    public event Action<string> ButtonPressed; 
    private void Start()
    {
        button.onClick.AddListener(OnButtonPressed);
    }

    private  void OnButtonPressed()
    {
        ButtonPressed?.Invoke(safeButtonNumber);
    }
}
