using UnityEngine;

public class SettingsUI : MainCanvasPanel
{
    [Header("Panels")]
    [Space]
    [SerializeField] private PanelActivationManager mainSettingsPanel;
    [SerializeField] private PanelActivationManager chooseLanguagePanel;
    [SerializeField] private PanelActivationManager infoPanel;

    private void Start()
    {
        SetStartSettings();
    }

    public void ShowPanel_MainPanel()
    {
        mainSettingsPanel.ShowPanel();
        chooseLanguagePanel.HidePanel();
        infoPanel.HidePanel();
    }

    public void ShowPanel_ChangeLanguagePanel()
    {
        mainSettingsPanel.HidePanel();
        chooseLanguagePanel.ShowPanel();
        infoPanel.HidePanel();
    }

    public void ShowPanel_InfoPanel()
    {
        mainSettingsPanel.HidePanel();
        chooseLanguagePanel.HidePanel();
        infoPanel.ShowPanel();
    }

    private void SetStartSettings()
    {
        ShowPanel_MainPanel();
    }
}
