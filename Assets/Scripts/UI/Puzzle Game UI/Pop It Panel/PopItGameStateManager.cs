using System;
using System.Collections.Generic;
using UnityEngine;

public class PopItGameStateManager : MonoBehaviour
{
    [Header("Buttons")]
    [Space]
    [SerializeField] private List<PopItButton> buttonsList = new List<PopItButton>();

    private bool finished = false;

    public bool Finished { get => finished; private set => finished = value; }

    private Action OnGameFinished;

    private void Start()
    {
        for(int i = 0; i < buttonsList.Count; i++)
        {
            buttonsList[i].CashComponents(this);
        }
    }

    public void StartGame(Action OnGameComplete)
    {
        OnGameFinished = OnGameComplete;
    }    

    public void CheckButtonsState()
    {
        bool allButtonsInCorrectPos = true;

        for(int i = 0; i < buttonsList.Count; i++)
        {
            if(!buttonsList[i].IsInCorrectPos)
            {
                allButtonsInCorrectPos = false;
                break;
            }
        }

        if(allButtonsInCorrectPos)
        {
            OnGameFinished?.Invoke();
        }
    }

    public void ResetGame()
    {
        finished = false;
        for(int i = 0; i < buttonsList.Count; i++)
        {
            buttonsList[i].ResetButton();
        }
    }
}
