using UnityEngine;

public class SettingsUI : MainCanvasPanel
{
    [Header("Panels")]
    [Space]
    [SerializeField] private PanelActivationManager mainSettingsPanel;
    [SerializeField] private PanelActivationManager chooseLanguagePanel;
    [SerializeField] private PanelActivationManager infoPanel;
    [SerializeField] private PanelActivationManager privacyPolicyPanel;

    private void Start()
    {
        SetStartSettings();
    }

    public void ShowPanel_MainPanel()
    {
        mainSettingsPanel.ShowPanel();
        chooseLanguagePanel.HidePanel();
        infoPanel.HidePanel();
        privacyPolicyPanel.HidePanel();
    }

    public void ShowPanel_ChangeLanguagePanel()
    {
        mainSettingsPanel.HidePanel();
        chooseLanguagePanel.ShowPanel();
        infoPanel.HidePanel();
        privacyPolicyPanel.HidePanel();
    }

    public void ShowPanel_InfoPanel()
    {
        mainSettingsPanel.HidePanel();
        chooseLanguagePanel.HidePanel();
        infoPanel.ShowPanel();
        privacyPolicyPanel.HidePanel();
    }

    public void ShowPanel_PrivacyPolicyUI()
    {
        mainSettingsPanel.HidePanel();
        chooseLanguagePanel.HidePanel();
        infoPanel.HidePanel();
        privacyPolicyPanel.ShowPanel();
    }

    private void SetStartSettings()
    {
        ShowPanel_MainPanel();
    }
}
