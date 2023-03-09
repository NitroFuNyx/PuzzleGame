using UnityEngine;

public class SettingsUI : MainCanvasPanel
{
    [Header("Panels")]
    [Space]
    [SerializeField] private PanelActivationManager mainSettingsPanel;
    [SerializeField] private PanelActivationManager chooseLanguagePanel;

    private void Start()
    {
        SetStartSettings();
    }

    public void ShowPanel_ChangeLanguagePanel()
    {
        mainSettingsPanel.HidePanel();
        chooseLanguagePanel.ShowPanel();
    }

    private void SetStartSettings()
    {
        mainSettingsPanel.ShowPanel();
        chooseLanguagePanel.HidePanel();
    }
}
