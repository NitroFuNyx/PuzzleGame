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

    private const string puzzleCode_Room = "631128";
    private const string puzzleCode_Backyard = "631128";

    private PuzzleGameUI _puzzleGameUI;

    private Color startColor;

    public Action _OnGameFinished;

    private void Start()
    {
        StartCoroutine(LateStart());
        startColor = safeString.color;
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
                Haptic.Vibrate();
                safeString.text = "Error";
                safeString.color = Color.red;
                StartCoroutine(DeletingText());
            }
        }
    }

    public void StartGame(Action OnGameFinished)
    {
        _OnGameFinished = OnGameFinished;
    }

    public void ResetGame()
    {
        safeString.text = "";
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
        safeString.color = startColor;
    }

}