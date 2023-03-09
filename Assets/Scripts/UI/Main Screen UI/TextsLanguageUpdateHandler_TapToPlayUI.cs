using UnityEngine;
using TMPro;

public class TextsLanguageUpdateHandler_TapToPlayUI : TextsLanguageUpdateHandler
{
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI tiltleText;

    public override void OnLanguageChange_ExecuteReaction(LanguageTextsHolder languageHolder)
    {
        tiltleText.text = languageHolder.data.mainscreenUITexts.titleText;
    }
}
