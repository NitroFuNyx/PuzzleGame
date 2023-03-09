using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    [Header("Texts JSON")]
    [Space]
    [SerializeField] private TextAsset englishTextsJSON;
    [SerializeField] private TextAsset ukrainianTextsJSON;
    [SerializeField] private TextAsset spanishTextsJSON;

    private LanguageTextsHolder englishTextsHolder;
    private LanguageTextsHolder ukrainianTextsHolder;
    private LanguageTextsHolder spanishTextsHolder;

    private void Start()
    {
        SetLanguageReader(englishTextsHolder, englishTextsJSON);
        SetLanguageReader(ukrainianTextsHolder, ukrainianTextsJSON);
        SetLanguageReader(spanishTextsHolder, spanishTextsJSON);
    }

    private void SetLanguageReader(LanguageTextsHolder textsHolder, TextAsset json)
    {
        LanguageTextsHolder holder = new LanguageTextsHolder();
        holder = JsonUtility.FromJson<LanguageTextsHolder>(json.text);

        textsHolder = holder;

        Debug.Log($"titleText {textsHolder.data.settingsUITexts.languageButtonText}");
    }
}
