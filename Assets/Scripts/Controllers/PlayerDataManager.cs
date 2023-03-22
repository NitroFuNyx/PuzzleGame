using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using Zenject;

public class PlayerDataManager : MonoBehaviour, IDataPersistance
{
    [Header("Language Data")]
    [Space]
    [SerializeField] private Languages currentLanguage;
    [Header("Resources Data")]
    [Space]
    [SerializeField] private int currentCoinsAmount = 0;
    [SerializeField] private int miniGameLevelHighestScore = 0;
    [Header("Audio Data")]
    [Space]
    [SerializeField] private bool soundMuted;

    [SerializeField] private int miniGameLevelStateIndex;
    [SerializeField] private ChooseGameLevelPanel minigamePanel;
    [SerializeField] private List<ChooseGameLevelPanel> miniGameLevelsPanelsList = new List<ChooseGameLevelPanel>();

    private List<Languages> allLanguagesList = new List<Languages>();

    private LanguageManager _languageManager;
    private AudioManager _audioManager;
    private ResourcesManager _resourcesManager;

    private CurrentGameManager _currentGameManager;

    public Languages CurrentLanguage { get => currentLanguage; private set => currentLanguage = value; }
    public int CurrentCoinsAmount { get => currentCoinsAmount; private set => currentCoinsAmount = value; }
    public bool SoundMuted { get => soundMuted; private set => soundMuted = value; }
    public int MiniGameLevelHighestScore { get => miniGameLevelHighestScore; private set => miniGameLevelHighestScore = value; }
    public int MiniGameLevelStateIndex { get => miniGameLevelStateIndex; set => miniGameLevelStateIndex = value; }
    public ChooseGameLevelPanel MinigamePanel { get => minigamePanel; set => minigamePanel = value; }
    public List<ChooseGameLevelPanel> MiniGameLevelsPanelsList { get => miniGameLevelsPanelsList; set => miniGameLevelsPanelsList = value; }

    #region Events Declaration
    public event Action OnPlayerMainDataLoaded;
    #endregion Events Declaration

    private void Awake()
    {
        SetStartSettings();
        FindObjectOfType<DataPersistanceManager>().AddObjectToSaveSystemObjectsList(this);
    }

    #region Zenject
    [Inject]
    private void Construct(LanguageManager languageManager, AudioManager audioManager, ResourcesManager resourcesManager, CurrentGameManager currentGameManager)
    {
        _languageManager = languageManager;
        _audioManager = audioManager;
        _resourcesManager = resourcesManager;
        _currentGameManager = currentGameManager;
    }
    #endregion Zenject

    //[ContextMenu("Save Player Data")]
    //public void SavePlayerData()
    //{
    //    currentCoinsAmount = _resourcesManager.WholeCoinsAmount;

    //    if(miniGameLevelHighestScore < _resourcesManager.CurrentLevelCoinsAmount)
    //    {
    //        miniGameLevelHighestScore = _resourcesManager.CurrentLevelCoinsAmount;
    //    }

    //    miniGameLevelStateIndex = (int)minigamePanel.LevelState;

    //    SaveLoadSystem.SavePlayerData(this);
    //}

    //[ContextMenu("Load Player Data")]
    //public void LoadPlayerData()
    //{
    //    PlayerMainData dataHolder = SaveLoadSystem.LoadPlayerData();

    //    if(dataHolder != null)
    //    {
    //        for (int i = 0; i < allLanguagesList.Count; i++)
    //        {
    //            if ((int)allLanguagesList[i] == dataHolder.languageIndex)
    //            {
    //                currentLanguage = allLanguagesList[i];
    //                break;
    //            }
    //        }

    //        soundMuted = dataHolder.soundMuted;
    //        currentCoinsAmount = dataHolder.currentCoinsAmount;
    //        miniGameLevelHighestScore = dataHolder.miniGameLevelHighestScore;

    //        miniGameLevelStateIndex = dataHolder.miniGameLevelStateIndex;

    //        minigamePanel.SetLevelStateIndex(miniGameLevelStateIndex);

    //        OnPlayerMainDataLoaded.Invoke();
    //    }
    //    else
    //    {
    //        Debug.Log("No savings");
    //        SaveLoadSystem.SavePlayerData(this);
    //        StartCoroutine(LoadMainDataCoroutine());
    //    }
    //}

    public void SaveLanguageData(Languages language)
    {
        currentLanguage = language;
        //SavePlayerData();
    }

    public void SaveAudioData(bool audioMuted)
    {
        soundMuted = audioMuted;
        //SavePlayerData();
    }

    private void SetStartSettings()
    {
        FillLanguagesList();
        //StartCoroutine(LoadMainDataCoroutine());
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
        //LoadPlayerData();
    }

    public void LoadData(GameData data)
    {
        
    }

    public void SaveData(GameData data)
    {
        //data.miniGameLevelsDataList[0].highestScore = 2;
    }
}
