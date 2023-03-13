using UnityEngine;
using Localization;
using TMPro;

public class TextsLanguageUpdateHandler_SettingsUI : TextsLanguageUpdateHandler
{
    [Header("Main Panel Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI mainPanelTiltleText;
    [SerializeField] private TextMeshProUGUI soundsButtonText;
    [SerializeField] private TextMeshProUGUI changeLanguageButtonText;
    [SerializeField] private TextMeshProUGUI infoButtonText;
    [Header("Choose Language Panel Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI chooseLanguagePanelTitleText;
    [Header("Info Panel Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI infoPanelTitleText;
    [SerializeField] private TextMeshProUGUI infoPanelDescriptionText;

    public override void OnLanguageChange_ExecuteReaction(LanguageTextsHolder languageHolder)
    {
        mainPanelTiltleText.text = languageHolder.data.settingsUITexts.mainSettingsPanel.uiTitleText;
        soundsButtonText.text = languageHolder.data.settingsUITexts.mainSettingsPanel.soundsButtonText;
        changeLanguageButtonText.text = languageHolder.data.settingsUITexts.mainSettingsPanel.languageButtonText;
        infoButtonText.text = languageHolder.data.settingsUITexts.mainSettingsPanel.infoButtonText;

        chooseLanguagePanelTitleText.text = languageHolder.data.settingsUITexts.changeLanguagePanel.uiTitleText;

        infoPanelTitleText.text = languageHolder.data.settingsUITexts.infoPanel.uiTitleText;
        infoPanelDescriptionText.text = languageHolder.data.settingsUITexts.infoPanel.infoText;
    }
}
