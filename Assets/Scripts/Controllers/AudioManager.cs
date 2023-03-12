using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [Space]
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource voicesAudioSource;

    private List<AudioSource> audioSourcesList = new List<AudioSource>();

    private PlayerDataManager _playerDataManager;

    //private bool audioMuted;

    #region Events Declaration
    public event Action<bool> OnAudioMuteStateChanged;
    #endregion Events Declaration

    private void Start()
    {
        SetStartSettings();
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

    public void ChangeMuteState(bool muted)
    {
        for (int i = 0; i < audioSourcesList.Count; i++)
        {
            audioSourcesList[i].mute = muted;
        }
        _playerDataManager.SaveAudioData(muted);
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
        _playerDataManager.OnPlayerMainDataLoaded += PlayerMainDataLoaded_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        _playerDataManager.OnPlayerMainDataLoaded -= PlayerMainDataLoaded_ExecuteReaction;
    }

    private void PlayerMainDataLoaded_ExecuteReaction()
    {
        for (int i = 0; i < audioSourcesList.Count; i++)
        {
            audioSourcesList[i].mute = _playerDataManager.SoundMuted;
        }
        OnAudioMuteStateChanged?.Invoke(_playerDataManager.SoundMuted);
    }
}
