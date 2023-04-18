using UnityEngine;
using Zenject;

public class CharacterInteractionHandler : MonoBehaviour, Iinteractable
{
    [Header("Chracter Type")]
    [Space]
    [SerializeField] private CharacterTypes characterType;

    private AudioManager _audioManager;

    private Animator animator;

    private int emotionsAmount = 2;

    private bool isGirl = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if(characterType == CharacterTypes.Female)
        {
            isGirl = true;
        }
        else
        {
            isGirl = false;
        }
    }

    #region Zenject
    [Inject]
    private void Construct(AudioManager audioManager)
    {
        _audioManager = audioManager;
    }
    #endregion Zenject

    public void Interact()
    {
        ChangeEmotion();
        _audioManager.PlayVoicesAudio_CharacterInteraction(characterType);
    }

    private void ChangeEmotion()
    {
        int emotionIndex = Random.Range(0, emotionsAmount);
        string emotion = "";
        if(emotionIndex == 0)
        {
            emotion = EmotionAnimations.Shocked;
        }
        else
        {
            emotion = EmotionAnimations.Cunning;
        }
        
        animator.SetBool(EmotionAnimations.IsGirl, isGirl);
        animator.SetTrigger(emotion);
    }
}
