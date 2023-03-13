using System.Collections.Generic;
using UnityEngine;
using System;
using Localization;
using Zenject;

public class LanguageManager : MonoBehaviour
{
    [Header("Texts JSON")]
    [Space]
    [SerializeField] private TextAsset englishTextsJSON;
    [SerializeField] private TextAsset ukrainianTextsJSON;
    [SerializeField] private TextAsset spanishTextsJSON;

    private LanguageTextsHolder englishTextsHolder = new LanguageTextsHolder();
    private LanguageTextsHolder ukrainianTextsHolder = new LanguageTextsHolder();
    private LanguageTextsHolder spanishTextsHolder = new LanguageTextsHolder();

    private Dictionary<Languages, LanguageTextsHolder> languagesHoldersDictionary = new Dictionary<Languages, LanguageTextsHolder>();

    private PlayerDataManager _playerDataManager;

    #region Events Declaration
    public event Action<LanguageTextsHolder> OnLanguageChanged;
    #endregion Events Declaration

    private void Start()
    {
        FillLanguageTextHolders();
        FillLanguagesHoldersDictionary();

        SubscribeOnEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    #region Zenject
    [Inject]
    private void Construct(PlayerDataManager playerDataManager)
    {
        _playerDataManager = playerDataManager;
    }
    #endregion Zenject

    public void ChangeLanguage(Languages language)
    {
        if(languagesHoldersDictionary.ContainsKey(language))
        {
            OnLanguageChanged?.Invoke(languagesHoldersDictionary[language]);
            _playerDataManager.SaveLanguageData(language);
        }
    }

    private LanguageTextsHolder SetLanguageTextHolder(TextAsset json)
    {
        LanguageTextsHolder holder = new LanguageTextsHolder();
        holder = JsonUtility.FromJson<LanguageTextsHolder>(json.text);

        return holder;
    }

    private void FillLanguageTextHolders()
    {
        englishTextsHolder = SetLanguageTextHolder(englishTextsJSON);
        ukrainianTextsHolder = SetLanguageTextHolder(ukrainianTextsJSON);
        spanishTextsHolder = SetLanguageTextHolder(spanishTextsJSON);
    }

    private void FillLanguagesHoldersDictionary()
    {
        languagesHoldersDictionary.Add(Languages.English, englishTextsHolder);
        languagesHoldersDictionary.Add(Languages.Ukrainian, ukrainianTextsHolder);
        languagesHoldersDictionary.Add(Languages.Spanish, spanishTextsHolder);
    }

    private void SubscribeOnEvents()
    {
        _playerDataManager.OnPlayerMainDataLoaded += PlayerMainDataLoaded_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        _playerDataManager.OnPlayerMainDataLoaded -= PlayerMainDataLoaded_ExecuteReaction;
    }

    private void PlayerMainDataLoaded_ExecuteReaction()
    {
        ChangeLanguage(_playerDataManager.CurrentLanguage);
    }    
}
