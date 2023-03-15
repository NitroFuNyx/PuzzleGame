using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using Zenject;

public class PlayerDataManager : MonoBehaviour
{
    [Header("Language Data")]
    [Space]
    [SerializeField] private Languages currentLanguage;
    [Header("Resources Data")]
    [Space]
    [SerializeField] private int currentCoinsAmount = 0;
    [Header("Audio Data")]
    [Space]
    [SerializeField] private bool soundMuted;

    private List<Languages> allLanguagesList = new List<Languages>();

    private LanguageManager _languageManager;
    private AudioManager _audioManager;
    private ResourcesManager _resourcesManager;

    public Languages CurrentLanguage { get => currentLanguage; private set => currentLanguage = value; }
    public int CurrentCoinsAmount { get => currentCoinsAmount; private set => currentCoinsAmount = value; }
    public bool SoundMuted { get => soundMuted; private set => soundMuted = value; }

    #region Events Declaration
    public event Action OnPlayerMainDataLoaded;
    #endregion Events Declaration

    private void Awake()
    {
        SetStartSettings();
    }

    #region Zenject
    [Inject]
    private void Construct(LanguageManager languageManager, AudioManager audioManager, ResourcesManager resourcesManager)
    {
        _languageManager = languageManager;
        _audioManager = audioManager;
        _resourcesManager = resourcesManager;
    }
    #endregion Zenject

    [ContextMenu("Save Player Data")]
    public void SavePlayerData()
    {
        currentCoinsAmount = _resourcesManager.WholeCoinsAmount;
        SaveLoadSystem.SavePlayerData(this);
    }

    [ContextMenu("Load Player Data")]
    public void LoadPlayerData()
    {
        PlayerMainData dataHolder = SaveLoadSystem.LoadPlayerData();

        if(dataHolder != null)
        {
            for (int i = 0; i < allLanguagesList.Count; i++)
            {
                if ((int)allLanguagesList[i] == dataHolder.languageIndex)
                {
                    currentLanguage = allLanguagesList[i];
                    break;
                }
            }

            soundMuted = dataHolder.soundMuted;
            currentCoinsAmount = dataHolder.currentCoinsAmount;

            OnPlayerMainDataLoaded.Invoke();
        }
        else
        {
            Debug.Log("No savings");
            SaveLoadSystem.SavePlayerData(this);
            StartCoroutine(LoadMainDataCoroutine());
        }

        Debug.Log($"Load {dataHolder.currentCoinsAmount}");
    }

    public void SaveLanguageData(Languages language)
    {
        currentLanguage = language;
        SavePlayerData();
    }

    public void SaveAudioData(bool audioMuted)
    {
        soundMuted = audioMuted;
        SavePlayerData();
    }

    private void SetStartSettings()
    {
        FillLanguagesList();
        StartCoroutine(LoadMainDataCoroutine());
    }

    private void FillLanguagesList()
    {
        allLanguagesList.Add(Languages.English);
        allLanguagesList.Add(Languages.Ukrainian);
        allLanguagesList.Add(Languages.Spanish);
        allLanguagesList.Add(Languages.Other);
    }

    private IEnumerator LoadMainDataCoroutine()
    {
        yield return null;
        LoadPlayerData();
    }
}
