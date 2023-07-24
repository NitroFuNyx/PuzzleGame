using UnityEngine;
using Localization;
using TMPro;

public class TextsLanguageUpdateHandler_PuzzleUI : TextsLanguageUpdateHandler
{
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI panelTiltleText;
    [SerializeField] private TextMeshProUGUI inJustText;
    [SerializeField] private TextMeshProUGUI finishTimeText;
    [SerializeField] private TextMeshProUGUI playAgainButtonText;

    public override void OnLanguageChange_ExecuteReaction(LanguageTextsHolder languageHolder)
    {
        panelTiltleText.text = languageHolder.data.puzzleUITexts.uiTitleText;
        inJustText.text = languageHolder.data.puzzleUITexts.inJustText;
        finishTimeText.text = languageHolder.data.puzzleUITexts.finishTimeText;
        playAgainButtonText.text = languageHolder.data.puzzleUITexts.playAgainButtonText;
        
    }
}
