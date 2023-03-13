using UnityEngine;
using Localization;
using TMPro;

public class TextsLanguageUpdateHandler_SelectPuzzleLevelUI : TextsLanguageUpdateHandler
{
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI panelTiltleText;

    public override void OnLanguageChange_ExecuteReaction(LanguageTextsHolder languageHolder)
    {
        panelTiltleText.text = languageHolder.data.choosePuzzleLevelUITexts.uiTitleText;
    }
}
