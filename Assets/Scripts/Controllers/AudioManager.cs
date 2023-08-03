using System.Collections.Generic;
using System.Collections;
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
    [SerializeField] private AudioClip kidGreetingsClip;
    [SerializeField] private AudioClip kidGivesKeyClip;
    [SerializeField] private List<AudioClip> oldManGivesKeyClipsList = new List<AudioClip>();
    [Header("Female Voice Clips")]
    [Space]
    [SerializeField] private List<AudioClip> touchAudioClipsList_Female = new List<AudioClip>();
    [SerializeField] private List<AudioClip> openLockClipsList_Female = new List<AudioClip>();
    [SerializeField] private AudioClip cakeAudioClip_Female;
    [Header("Male Voice Clips")]
    [Space]
    [SerializeField] private List<AudioClip> touchAudioClipsList_Male = new List<AudioClip>();
    [SerializeField] private List<AudioClip> openLockClipsList_Male = new List<AudioClip>();
    [SerializeField] private AudioClip cakeAudioClip_Male;
    [Header("Old Man Voice Clips")]
    [Space]
    [SerializeField] private List<AudioClip> touchAudioClipsList_OldMan = new List<AudioClip>();
    [Space]
    [SerializeField] private AudioClip uiButtonClip;

    private List<AudioSource> audioSourcesList = new List<AudioSource>();

    private List<AudioClip> openLockClipsList_Available = new List<AudioClip>();
    private List<AudioClip> openLockClipsList_Used = new List<AudioClip>();

    private List<AudioClip> interactFemaleClipsList_Available = new List<AudioClip>();
    private List<AudioClip> interactFemaleClipsList_Used = new List<AudioClip>();

    private List<AudioClip> interactMaleClipsList_Available = new List<AudioClip>();
    private List<AudioClip> interactMaleClipsList_Used = new List<AudioClip>();

    private List<AudioClip> interactOldManClipsList_Available = new List<AudioClip>();
    private List<AudioClip> interactOldManClipsList_Used = new List<AudioClip>();

    private DataPersistanceManager _dataPersistanceManager;

    private bool audioMuted = false;

    private float kidGivesKeyDelay = 0.5f;

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

        FillOpenLockClipsList();
        FillInteractClipsList_Female();
        FillInteractClipsList_Male();
        FillInteractClipsList_OldMan();
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


    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus) return;
        Debug.Log($"Music clip {musicAudioSource.clip}");
        musicAudioSource.Stop();
        musicAudioSource.clip = mainUIMusicClip;
        musicAudioSource.Play();
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

    public void PlayVoicesAudio_KidGivesKey()
    {
        voicesAudioSource.clip = kidGreetingsClip;
        voicesAudioSource.Play();
        StartCoroutine(FinishKidGivesKeyClipCoroutine());
    }

    public void PlayVoicesAudio_OldManGivesKey()
    {
        int index = UnityEngine.Random.Range(0, oldManGivesKeyClipsList.Count);
        voicesAudioSource.clip = oldManGivesKeyClipsList[index];
        voicesAudioSource.Play();
    }

    public void PlayVoicesAudio_OpenLock()
    {
        AudioClip clip = openLockClipsList_Female[0];
        AudioSource source = femaleVoiceAudioSource;

        if (openLockClipsList_Available.Count == 0)
        {
            openLockClipsList_Used.Clear();
            openLockClipsList_Available.Clear();
            FillOpenLockClipsList();
        }

        int index = UnityEngine.Random.Range(0, openLockClipsList_Available.Count);
        clip = openLockClipsList_Available[index];
        openLockClipsList_Available.Remove(clip);
        openLockClipsList_Used.Add(clip);

        if (openLockClipsList_Female.Contains(clip))
        {
            source = femaleVoiceAudioSource;
        }
        else if (openLockClipsList_Male.Contains(clip))
        {
            source = maleVoiceAudioSource;
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

            if (interactFemaleClipsList_Available.Count == 0)
            {
                interactFemaleClipsList_Used.Clear();
                interactFemaleClipsList_Available.Clear();
                FillInteractClipsList_Female();                
            }

            int femaleindex = UnityEngine.Random.Range(0, interactFemaleClipsList_Available.Count);
            clip = interactFemaleClipsList_Available[femaleindex];
            interactFemaleClipsList_Available.Remove(clip);
            interactFemaleClipsList_Used.Add(clip);
        }
        else if(character == CharacterTypes.Male)
        {
            source = maleVoiceAudioSource;

            if (interactMaleClipsList_Available.Count == 0)
            {
                interactMaleClipsList_Used.Clear();
                interactMaleClipsList_Available.Clear();
                FillInteractClipsList_Male();
            }

            int maleindex = UnityEngine.Random.Range(0, interactMaleClipsList_Available.Count);
            clip = interactMaleClipsList_Available[maleindex];
            interactMaleClipsList_Available.Remove(clip);
            interactMaleClipsList_Used.Add(clip);
        }
        else if (character == CharacterTypes.OldMan)
        {
            source = maleVoiceAudioSource;

            if (interactOldManClipsList_Available.Count == 0)
            {
                interactOldManClipsList_Used.Clear();
                interactOldManClipsList_Available.Clear();
                FillInteractClipsList_OldMan();
            }

            int oldManindex = UnityEngine.Random.Range(0, interactOldManClipsList_Available.Count);
            clip = interactOldManClipsList_Available[oldManindex];
            interactOldManClipsList_Available.Remove(clip);
            interactOldManClipsList_Used.Add(clip);
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

    private void FillOpenLockClipsList()
    {
        openLockClipsList_Available.Clear();

        for (int i = 0; i < openLockClipsList_Female.Count; i++)
        {
            openLockClipsList_Available.Add(openLockClipsList_Female[i]);
        }
        for (int i = 0; i < openLockClipsList_Male.Count; i++)
        {
            openLockClipsList_Available.Add(openLockClipsList_Male[i]);
        }
    }

    private void FillInteractClipsList_Female()
    {
        interactFemaleClipsList_Available.Clear();

        for (int i = 0; i < touchAudioClipsList_Female.Count; i++)
        {
            interactFemaleClipsList_Available.Add(touchAudioClipsList_Female[i]);
        }
    }

    private void FillInteractClipsList_Male()
    {
        interactMaleClipsList_Available.Clear();

        for (int i = 0; i < touchAudioClipsList_Male.Count; i++)
        {
            interactMaleClipsList_Available.Add(touchAudioClipsList_Male[i]);
        }
    }

    private void FillInteractClipsList_OldMan()
    {
        interactOldManClipsList_Available.Clear();

        for (int i = 0; i < touchAudioClipsList_OldMan.Count; i++)
        {
            interactOldManClipsList_Available.Add(touchAudioClipsList_OldMan[i]);
        }
    }

    private IEnumerator FinishKidGivesKeyClipCoroutine()
    {
        yield return new WaitForSeconds(kidGivesKeyDelay);
        voicesAudioSource.clip = kidGivesKeyClip;
        voicesAudioSource.Play();
    }
}
