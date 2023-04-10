using UnityEngine;
using Localization;
using TMPro;

public class TextsLanguageUpdateHandler_MiniGameUI : TextsLanguageUpdateHandler
{
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI panelTiltleText;
    [SerializeField] private TextMeshProUGUI videoTiltleText;

    public override void OnLanguageChange_ExecuteReaction(LanguageTextsHolder languageHolder)
    {
        panelTiltleText.text = languageHolder.data.miniGameUITexts.uiTitleText;
        videoTiltleText.text = languageHolder.data.miniGameUITexts.videoText;
    }
}
