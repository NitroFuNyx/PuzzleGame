using UnityEngine;
using Zenject;

public class OldManInteractionHandler_Backyard : MonoBehaviour, Iinteractable
{
    private AudioManager _audioManager;

    private Animator animator;

    private bool canInteract = true;

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
        if(canInteract)
        {
            canInteract = false;

            interactionIndex++;

            _audioManager.PlayVoicesAudio_BackyardPuzzle_OldManInteraction(interactionIndex, ResetInteractionState);

            if (interactionIndex == 2)
            {
                // give key
            }
        }
    }

    public void ResetInteractionState()
    {
        canInteract = true;
    }
}
