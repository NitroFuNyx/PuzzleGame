using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    #region Events Declaration
    public event Action<LanguageTextsHolder> OnLanguageChanged;
    #endregion Events Declaration

    private void Start()
    {
        FillLanguageTextHolders();
    }

    private void SetLanguageTextHolder(LanguageTextsHolder textsHolder, TextAsset json)
    {
        LanguageTextsHolder holder = new LanguageTextsHolder();
        holder = JsonUtility.FromJson<LanguageTextsHolder>(json.text);

        textsHolder = holder;

        Debug.Log($"titleText {textsHolder.data.settingsUITexts.languageButtonText}");
    }

    private void FillLanguageTextHolders()
    {
        SetLanguageTextHolder(englishTextsHolder, englishTextsJSON);
        SetLanguageTextHolder(ukrainianTextsHolder, ukrainianTextsJSON);
        SetLanguageTextHolder(spanishTextsHolder, spanishTextsJSON);
    }
}
