using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    [Header("UI Panels")]
    [Space]
    [SerializeField] private MainLoaderUI mainLoaderUI;
    [SerializeField] private TapToPlayUI tapToPlayUI;
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

    private void FillPanelsList()
    {
        panelsList.Add(mainLoaderUI);
        panelsList.Add(tapToPlayUI);
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
        ActivateMainCanvasPanel(UIPanels.TapToPlayPanel);
        mainLoaderUI.ResetUIData();
    }
}
