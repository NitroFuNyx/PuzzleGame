using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    [Header("UI Panels")]
    [Space]
    [SerializeField] private MainLoaderUI mainLoaderUI;
    [SerializeField] private MainScreenUI mainScreenUI;
    [SerializeField] private SettingsUI settingsUI;
    [SerializeField] private SelectModeUI selectModeUI;

    private List<MainCanvasPanel> panelsList = new List<MainCanvasPanel>();

    private void Awake()
    {
        FillPanelsList();
    }

    private void Start()
    {
        SetStartSettings();
    }

    #region Buttons Methods
    public void ShowMainScreenUI()
    {
        ActivateMainCanvasPanel(UIPanels.MainScreenPanel);
        mainScreenUI.ActivateIdleAnimation();
    }

    public void ShowSettingsUI()
    {
        ActivateMainCanvasPanel(UIPanels.SettingsPanel);
        mainScreenUI.StopIdleAnimation();
    }

    public void ShowSelectGameModeUI()
    {
        ActivateMainCanvasPanel(UIPanels.SelectModePanel);
        mainScreenUI.StopIdleAnimation();
    }
    #endregion Buttons Methods

    private void FillPanelsList()
    {
        panelsList.Add(mainLoaderUI);
        panelsList.Add(mainScreenUI);
        panelsList.Add(settingsUI);
        panelsList.Add(selectModeUI);
    }

    private void SetStartSettings()
    {
        ActivateMainCanvasPanel(UIPanels.MainLoaderPanel);
        mainLoaderUI.StartLoadingAnimation(OnLoadingAnimationFinishedCallback);
    }

    private void ActivateMainCanvasPanel(UIPanels panel)
    {
        for(int i = 0; i < panelsList.Count; i++)
        {
            if(panelsList[i].PanelType == panel)
            {
                panelsList[i].ShowPanel();
            }
            else
            {
                panelsList[i].HidePanel();
            }
        }
    }

    private void OnLoadingAnimationFinishedCallback()
    {
        ShowMainScreenUI();
        mainLoaderUI.ResetUIData();
    }
}
