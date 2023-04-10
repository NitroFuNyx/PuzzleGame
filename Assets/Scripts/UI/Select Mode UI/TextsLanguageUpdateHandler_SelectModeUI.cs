using UnityEngine;
using Localization;
using TMPro;
using System.Collections.Generic;

public class TextsLanguageUpdateHandler_SelectModeUI : TextsLanguageUpdateHandler
{
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI tiltleText;
    [SerializeField] private TextMeshProUGUI puzzleText;
    [SerializeField] private TextMeshProUGUI minigameText;
    [Header("Panels")]
    [Space]
    [SerializeField] private List<ChooseGameLevelPanel> panelsList = new List<ChooseGameLevelPanel>();

    private LanguageTextsHolder currentLanguageTextsHolder;

    public override void OnLanguageChange_ExecuteReaction(LanguageTextsHolder languageHolder)
    {
        tiltleText.text = languageHolder.data.selectGameModeUITexts.uiTitleText;
        puzzleText.text = languageHolder.data.selectGameModeUITexts.puzzlePanelText;
        minigameText.text = languageHolder.data.selectGameModeUITexts.miniGamePanelText;

        currentLanguageTextsHolder = languageHolder;

        for(int i = 0; i < panelsList.Count; i++)
        {
            panelsList[i].SetPanelUIData();
        }
    }

    public string GetTranslatedText(SelectModePanelProgressTexts text)
    {
        string translatedText = "";
        if(text == SelectModePanelProgressTexts.BestTime)
        {
            translatedText = $"{currentLanguageTextsHolder.data.choosePuzzleLevelUITexts.bestTimeText}";
        }
        else if (text == SelectModePanelProgressTexts.CurrentTime)
        {
            translatedText = $"{currentLanguageTextsHolder.data.choosePuzzleLevelUITexts.currentTimeText}";
        }
        else if (text == SelectModePanelProgressTexts.BestScore)
        {
            translatedText = $"{currentLanguageTextsHolder.data.chooseMiniGameLevelUITexts.bestScore}";
        }
        else if (text == SelectModePanelProgressTexts.Start)
        {
            translatedText = $"{currentLanguageTextsHolder.data.chooseMiniGameLevelUITexts.startText}";
        }

        return translatedText;
    }
}
