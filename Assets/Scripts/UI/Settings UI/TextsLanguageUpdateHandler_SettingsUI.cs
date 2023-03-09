using UnityEngine;
using TMPro;

public class TextsLanguageUpdateHandler_SettingsUI : TextsLanguageUpdateHandler
{
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI tiltleText;

    public override void OnLanguageChange_ExecuteReaction(LanguageTextsHolder languageHolder)
    {
        
    }
}
