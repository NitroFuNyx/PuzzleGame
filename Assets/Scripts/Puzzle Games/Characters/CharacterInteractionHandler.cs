using UnityEngine;

public class CharacterInteractionHandler : MonoBehaviour, Iinteractable
{
    [Header("Chracter Type")]
    [Space]
    [SerializeField] private CharacterTypes characterType;

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

    public void Interact()
    {
        ChangeEmotion();
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
