using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopItGameStateManager : MonoBehaviour
{
    [Header("Buttons")]
    [Space]
    [SerializeField] private List<PopItButton> buttonsList = new List<PopItButton>();

    private void Start()
    {
        for(int i = 0; i < buttonsList.Count; i++)
        {
            buttonsList[i].CashComponents(this);
        }
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

        Debug.Log($"{allButtonsInCorrectPos}");
        if(allButtonsInCorrectPos)
        {
            // get key
        }
    }
}
