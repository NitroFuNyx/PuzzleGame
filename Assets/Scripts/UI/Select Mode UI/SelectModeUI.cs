using UnityEngine;
using System.Collections.Generic;

public class SelectModeUI : MainCanvasPanel
{
    [Header("Level Panels")]
    [Space]
    [SerializeField] private List<ChooseGameLevelPanel> panelsList = new List<ChooseGameLevelPanel>();

    public void StartPanelsAnimations()
    {
        for(int i = 0; i < panelsList.Count; i++)
        {
            panelsList[i].ShowBuyingPossibilityState();
        }
    }

    public void StopPanelsAnimations()
    {
        for (int i = 0; i < panelsList.Count; i++)
        {
            panelsList[i].StopBuyingPossibilityAnimation();
        }
    }
}
