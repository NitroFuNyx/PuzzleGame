using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class AudioManager : MonoBehaviour, IDataPersistance
{
    [Header("Audio Sources")]
    [Space]
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource voicesAudioSource;

    private List<AudioSource> audioSourcesList = new List<AudioSource>();

    private PlayerDataManager _playerDataManager;
    private DataPersistanceManager _dataPersistanceManager;

    private bool audioMuted = false;

    #region Events Declaration
    public event Action<bool> OnAudioMuteStateChanged;
    #endregion Events Declaration

    private void Start()
    {
        SetStartSettings();
        SubscribeOnEvents();

        _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    #region Zenject
    [Inject]
    private void Construct(PlayerDataManager playerDataManager, DataPersistanceManager dataPersistanceManager)
    {
        _playerDataManager = playerDataManager;
        _dataPersistanceManager = dataPersistanceManager;
    }
    #endregion Zenject

    public void ChangeMuteState(bool muted)
    {
        audioMuted = muted;

        for (int i = 0; i < audioSourcesList.Count; i++)
        {
            audioSourcesList[i].mute = muted;
        }
        _playerDataManager.SaveAudioData(muted);
        _dataPersistanceManager.SaveGame();
        OnAudioMuteStateChanged?.Invoke(muted);
    }

    private void SetStartSettings()
    {
        FillAudioSourcesList();
    }

    private void FillAudioSourcesList()
    {
        audioSourcesList.Add(musicAudioSource);
        audioSourcesList.Add(sfxAudioSource);
        audioSourcesList.Add(voicesAudioSource);
    }

    private void SubscribeOnEvents()
    {
        //_playerDataManager.OnPlayerMainDataLoaded += PlayerMainDataLoaded_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        //_playerDataManager.OnPlayerMainDataLoaded -= PlayerMainDataLoaded_ExecuteReaction;
    }

    private void PlayerMainDataLoaded_ExecuteReaction()
    {
        for (int i = 0; i < audioSourcesList.Count; i++)
        {
            audioSourcesList[i].mute = _playerDataManager.SoundMuted;
        }
        OnAudioMuteStateChanged?.Invoke(_playerDataManager.SoundMuted);
    }

    public void LoadData(GameData data)
    {
        audioMuted = data.soundMuted;
        Debug.Log(data.currentCoinsAmount);
        Debug.Log($"{data.miniGameLevelsDataList[0].highestScore}");
    }

    public void SaveData(GameData data)
    {
        data.soundMuted = audioMuted;
    }
}
