using UnityEngine;
using Localization;
using TMPro;

public class TextsLanguageUpdateHandler_SelectMiniGameLevelUI : TextsLanguageUpdateHandler
{
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI panelTiltleText;

    public override void OnLanguageChange_ExecuteReaction(LanguageTextsHolder languageHolder)
    {
        panelTiltleText.text = languageHolder.data.chooseMiniGameLevelUITexts.uiTitleText;
    }
}
