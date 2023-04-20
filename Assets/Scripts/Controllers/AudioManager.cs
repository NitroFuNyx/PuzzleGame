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
    [SerializeField] private AudioSource sfxAdditionalAudioSource;
    [SerializeField] private AudioSource voicesAudioSource;
    [SerializeField] private AudioSource femaleVoiceAudioSource;
    [SerializeField] private AudioSource maleVoiceAudioSource;
    [Header("Music Clips")]
    [Space]
    [SerializeField] private AudioClip mainUIMusicClip;
    [SerializeField] private AudioClip miniGameMusicClip;
    [SerializeField] private AudioClip puzzleMusicClip;
    [Header("SFX Clips")]
    [Space]
    [SerializeField] private AudioClip openLockClip;
    [SerializeField] private AudioClip pickUpKeyClip;
    [SerializeField] private AudioClip appearedKeyClip;
    [SerializeField] private AudioClip puzzleItemInteractionClip;
    [SerializeField] private AudioClip pressSafeButtonClip;
    [SerializeField] private AudioClip pressMixerButtonClip;
    [SerializeField] private AudioClip lampInteractionClip;
    [SerializeField] private AudioClip appearedOldManClip;
    [Space]
    [SerializeField] private AudioClip miniGameDebuffInteractionClip;
    [SerializeField] private AudioClip miniGameCoinInteractionClip;
    [SerializeField] private AudioClip miniGameBonusInteractionClip;
    [SerializeField] private AudioClip miniGamePlayerStunClip;
    [Header("Common Voices Clips")]
    [Space]
    [SerializeField] private AudioClip levelFinishedVoicesClip;
    [Header("Female Voice Clips")]
    [Space]
    [SerializeField] private List<AudioClip> touchAudioClipsList_Female = new List<AudioClip>();
    [SerializeField] private AudioClip magicAudioClip_Female;
    [SerializeField] private AudioClip cakeAudioClip_Female;
    [Header("Male Voice Clips")]
    [Space]
    [SerializeField] private List<AudioClip> touchAudioClipsList_Male = new List<AudioClip>();
    [SerializeField] private AudioClip magicAudioClip_Male;
    [SerializeField] private AudioClip cakeAudioClip_Male;
    [Space]
    [SerializeField] private AudioClip uiButtonClip;

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
        audioSourcesList.Add(sfxAdditionalAudioSource);
        audioSourcesList.Add(voicesAudioSource);
        audioSourcesList.Add(femaleVoiceAudioSource);
        audioSourcesList.Add(maleVoiceAudioSource);
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

    #region Music Methods
    public void PlayMusic_MainUI()
    {
        Debug.Log($"Main {musicAudioSource.clip}");
        if(musicAudioSource.clip != mainUIMusicClip)
        {
            Debug.Log($"Music clip {musicAudioSource.clip}");
            musicAudioSource.Stop();
            musicAudioSource.clip = mainUIMusicClip;
            musicAudioSource.Play();
        }
        else if(musicAudioSource.clip == mainUIMusicClip && !musicAudioSource.isPlaying)
        {
            musicAudioSource.Stop();
            musicAudioSource.clip = mainUIMusicClip;
            musicAudioSource.Play();
        }
    }

    public void PlayMusic_MiniGame()
    {
        if (musicAudioSource.clip != miniGameMusicClip)
        {
            musicAudioSource.Stop();
            musicAudioSource.clip = miniGameMusicClip;
            musicAudioSource.Play();
        }
        else if (musicAudioSource.clip == miniGameMusicClip && !musicAudioSource.isPlaying)
        {
            musicAudioSource.Stop();
            musicAudioSource.clip = miniGameMusicClip;
            musicAudioSource.Play();
        }
    }

    public void PlayMusic_Puzzle()
    {
        if (musicAudioSource.clip != puzzleMusicClip)
        {
            musicAudioSource.Stop();
            musicAudioSource.clip = puzzleMusicClip;
            musicAudioSource.Play();
        }
        else if (musicAudioSource.clip == puzzleMusicClip && !musicAudioSource.isPlaying)
        {
            musicAudioSource.Stop();
            musicAudioSource.clip = puzzleMusicClip;
            musicAudioSource.Play();
        }
    }
    #endregion Music Methods

    #region SFX Methods
    public void PlaySFXSound_OpenLock()
    {
        AudioSource source = GetSFXAudioSource();
        sfxAudioSource.clip = openLockClip;
        sfxAudioSource.Play();
    }

    public void PlaySFXSound_PickUpKey()
    {
        AudioSource source = GetSFXAudioSource();
        source.clip = pickUpKeyClip;
        source.Play();
    }

    public void PlaySFXSound_PuzzleItemInteraction()
    {
        AudioSource source = GetSFXAudioSource();
        source.clip = puzzleItemInteractionClip;
        source.Play();
    }

    public void PlaySFXSound_PuzzleLampInteraction()
    {
        AudioSource source = GetSFXAudioSource();
        source.clip = lampInteractionClip;
        source.Play();
    }

    public void PlaySFXSound_OldManAppeared()
    {
        AudioSource source = GetSFXAudioSource();
        source.clip = appearedOldManClip;
        source.Play();
    }

    public void PlaySFXSound_KeyAppeared()
    {
        AudioSource source = GetSFXAudioSource();
        source.clip = appearedKeyClip;
        source.Play();
    }

    public void PlaySFXSound_PressSafeButton()
    {
        AudioSource source = GetSFXAudioSource();
        source.clip = pressSafeButtonClip;
        source.Play();
    }

    public void PlaySFXSound_PressMixerButton()
    {
        AudioSource source = GetSFXAudioSource();
        source.clip = pressMixerButtonClip;
        source.Play();
    }

    public void PlaySFXSound_PressButtonUI()
    {
        AudioSource source = GetSFXAudioSource();
        source.clip = uiButtonClip;
        source.Play();
    }

    public void PlaySFXSound_MiniGameDebuffInteraction()
    {
        AudioSource source = GetSFXAudioSource();
        source.clip = miniGameDebuffInteractionClip;
        source.Play();
    }

    public void PlaySFXSound_MiniGameCoinInteraction()
    {
        AudioSource source = GetSFXAudioSource();
        source.clip = miniGameCoinInteractionClip;
        source.Play();
    }

    public void PlaySFXSound_MiniGameBonusInteraction()
    {
        AudioSource source = GetSFXAudioSource();
        source.clip = miniGameBonusInteractionClip;
        source.Play();
    }

    public void PlaySFXSound_MiniGamePlayerStun()
    {
        AudioSource source = GetSFXAudioSource();
        source.clip = miniGamePlayerStunClip;
        source.Play();
    }

    public void StopSFXAudio()
    {
        sfxAudioSource.Stop();
        sfxAdditionalAudioSource.Stop();
    }
    #endregion SFX Methods

    #region Voices Methods
    public void PlayVoicesAudio_EndGame()
    {
        voicesAudioSource.clip = levelFinishedVoicesClip;
        voicesAudioSource.Play();
    }

    public void PlayVoicesAudio_MagicPhrase()
    {
        AudioSource source = GetSpeakerSource();
        AudioClip clip = magicAudioClip_Female;
        if(source == maleVoiceAudioSource)
        {
            clip = magicAudioClip_Male;
        }

        source.clip = clip;
        source.Play();
    }

    public void PlayVoicesAudio_CakePhrase()
    {
        AudioSource source = GetSpeakerSource();
        AudioClip clip = cakeAudioClip_Female;
        if (source == maleVoiceAudioSource)
        {
            clip = cakeAudioClip_Male;
        }

        source.clip = clip;
        source.Play();
    }

    public void PlayVoicesAudio_CharacterInteraction(CharacterTypes character)
    {
        AudioSource source = GetSpeakerSource();
        AudioClip clip = cakeAudioClip_Female;

        if (character == CharacterTypes.Female)
        {
            source = femaleVoiceAudioSource;

            int index = UnityEngine.Random.Range(0, touchAudioClipsList_Female.Count);
            clip = touchAudioClipsList_Female[index];
        }
        else
        {
            source = maleVoiceAudioSource;

            int index = UnityEngine.Random.Range(0, touchAudioClipsList_Male.Count);
            clip = touchAudioClipsList_Male[index];
        }

        source.clip = clip;
        source.Play();
    }

    public void StopVoicesAudio()
    {
        femaleVoiceAudioSource.Stop();
        maleVoiceAudioSource.Stop();
    }
    #endregion Voices Methods

    private AudioSource GetSFXAudioSource()
    {
        AudioSource source = sfxAudioSource;

        if(sfxAudioSource.isPlaying)
        {
            source = sfxAdditionalAudioSource;
        }

        return source;
    }

    private AudioSource GetSpeakerSource()
    {
        AudioSource source = femaleVoiceAudioSource;

        int index = UnityEngine.Random.Range(0, 2);

        if(index == 1)
        {
            source = maleVoiceAudioSource; ;
        }

        return source;
    }
}
