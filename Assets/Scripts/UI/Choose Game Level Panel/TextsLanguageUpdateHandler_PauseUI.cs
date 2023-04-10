using UnityEngine;
using Localization;
using TMPro;

public class TextsLanguageUpdateHandler_PauseUI : TextsLanguageUpdateHandler
{
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI panelTiltleText;
    [SerializeField] private TextMeshProUGUI exitText;
    [SerializeField] private TextMeshProUGUI continueText;

    public override void OnLanguageChange_ExecuteReaction(LanguageTextsHolder languageHolder)
    {
        panelTiltleText.text = languageHolder.data.pauseUITexts.uiTitleText;
        exitText.text = languageHolder.data.pauseUITexts.exitText;
        continueText.text = languageHolder.data.pauseUITexts.continueText;
    }
}
