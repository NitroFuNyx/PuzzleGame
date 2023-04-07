using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class SafeButtonsHandler : MonoBehaviour
{
    [SerializeField] private List<SafeButton> numButtons;
    [SerializeField] private TextMeshProUGUI safeString;
    
    private PuzzleGameUI _puzzleGameUI;

    public Action _OnGameFinished;

    private void Start()
    {
        StartCoroutine(LateStart());
    }

    #region Zenject
    [Inject]
    private void Construct(PuzzleGameUI puzzleGameUI)
    {
        _puzzleGameUI = puzzleGameUI;
    }
    #endregion Zenject

    public IEnumerator LateStart()
    {
        yield return new WaitForSeconds(.5f);
        foreach (var buttons in numButtons)
        {
            buttons.ButtonPressed += AddNumberToSafeString;
        }
    }

    public void AddNumberToSafeString(string Number)
    {
        if (safeString.text.Length < 6)
        {
            safeString.text += Number;
        }

        if (safeString.text.Length >= 6)
        {
            if (safeString.text == "631128")// Successfully written code
            {
                StartCoroutine(ClosingSafeUi());
            }
            else// Wrong written code
            {
                StartCoroutine(DeletingText());
            }
        }
    }

    public void StartGame(Action OnGameFinished)
    {
        _OnGameFinished = OnGameFinished;
    }

    public IEnumerator ClosingSafeUi()
    {
        yield return new WaitForSeconds(0.75f);
        _OnGameFinished?.Invoke();
        //_puzzleGameUI.ShowMainModePanel();
    }

    public IEnumerator DeletingText()
    {
        yield return new WaitForSeconds(0.75f);
        safeString.text = "";
    }

}