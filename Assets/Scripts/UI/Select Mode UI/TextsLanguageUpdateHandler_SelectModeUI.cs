using UnityEngine;
using Localization;
using TMPro;

public class TextsLanguageUpdateHandler_SelectModeUI : TextsLanguageUpdateHandler
{
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI tiltleText;
    [SerializeField] private TextMeshProUGUI puzzleText;
    [SerializeField] private TextMeshProUGUI minigameText;

    public override void OnLanguageChange_ExecuteReaction(LanguageTextsHolder languageHolder)
    {
        tiltleText.text = languageHolder.data.selectGameModeUITexts.uiTitleText;
        puzzleText.text = languageHolder.data.selectGameModeUITexts.puzzlePanelText;
        minigameText.text = languageHolder.data.selectGameModeUITexts.miniGamePanelText;
    }
}
