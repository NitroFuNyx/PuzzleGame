using UnityEngine;
using Zenject;

public class OldManInteractionHandler_Backyard : MonoBehaviour, Iinteractable
{
    private AudioManager _audioManager;

    private Animator animator;

    private int interactionIndex = 0;
    private int maxInteractionCount = 2;

    private void Awake()
    {
        animator = GetComponent<Animator>();
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
        interactionIndex++;

        _audioManager.PlayVoicesAudio_BackyardPuzzle_OldManInteraction(interactionIndex);


        if(interactionIndex == 2)
        {
            // give key
        }
    }

    //private void ChangeEmotion()
    //{
    //    int emotionIndex = Random.Range(0, emotionsAmount);
    //    string emotion = "";
    //    if (emotionIndex == 0)
    //    {
    //        emotion = EmotionAnimations.Shocked;
    //    }
    //    else
    //    {
    //        emotion = EmotionAnimations.Cunning;
    //    }

    //    animator.SetBool(EmotionAnimations.IsGirl, isGirl);
    //    animator.SetTrigger(emotion);
    //}
}
