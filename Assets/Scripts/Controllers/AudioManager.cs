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
    [Header("SFX Clips")]
    [Space]
    [SerializeField] private AudioClip openLockClip;
    [SerializeField] private AudioClip pickUpKeyClip;
    [SerializeField] private AudioClip uiButtonClip;
    [Space]
    [SerializeField] private AudioClip miniGameDebuffInteractionClip;
    [SerializeField] private AudioClip miniGameCoinInteractionClip;
    [SerializeField] private AudioClip miniGameBonusInteractionClip;

    private List<AudioSource> audioSourcesList = new List<AudioSource>();

    private DataPersistanceManager _dataPersistanceManager;

    private bool audioMuted = false;

    #region Events Declaration
    public event Action<bool> OnAudioMuteStateChanged;
    #endregion Events Declaration

    private void Start()
    {
        SetStartSettings();

        _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
    }

    #region Zenject
    [Inject]
    private void Construct(DataPersistanceManager dataPersistanceManager)
    {
        _dataPersistanceManager = dataPersistanceManager;
    }
    #endregion Zenject

    public void ChangeMuteState(bool muted)
    {
        audioMuted = muted;

        SetAudioSourcesState();
        _dataPersistanceManager.SaveGame();
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

    private void SetAudioSourcesState()
    {
        for (int i = 0; i < audioSourcesList.Count; i++)
        {
            audioSourcesList[i].mute = audioMuted;
        }

        OnAudioMuteStateChanged?.Invoke(audioMuted);
    }

    public void LoadData(GameData data)
    {
        audioMuted = data.soundMuted;
        SetAudioSourcesState();
    }

    public void SaveData(GameData data)
    {
        data.soundMuted = audioMuted;
    }

    #region SFX Methods
    public void PlaySFXSound_OpenLock()
    {
        sfxAudioSource.clip = openLockClip;
        sfxAudioSource.Play();
    }

    public void PlaySFXSound_PickUpKey()
    {
        sfxAudioSource.clip = pickUpKeyClip;
        sfxAudioSource.Play();
    }

    public void PlaySFXSound_PressButtonUI()
    {
        sfxAudioSource.clip = uiButtonClip;
        sfxAudioSource.Play();
    }

    public void PlaySFXSound_MiniGameDebuffInteraction()
    {
        sfxAudioSource.clip = miniGameDebuffInteractionClip;
        sfxAudioSource.Play();
    }

    public void PlaySFXSound_MiniGameCoinInteraction()
    {
        sfxAudioSource.clip = miniGameCoinInteractionClip;
        sfxAudioSource.Play();
    }

    public void PlaySFXSound_MiniGameBonusInteraction()
    {
        sfxAudioSource.clip = miniGameBonusInteractionClip;
        sfxAudioSource.Play();
    }
    #endregion SFX Methods
}
