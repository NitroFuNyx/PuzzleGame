using UnityEngine;
using System;
using Zenject;

public class PuzzleWindowCharacter : MonoBehaviour, Iinteractable
{
    [Header("Internal References")]
    [Space]
    [SerializeField] private WindowCharacterVFXManager characterVFXManager;

    private AudioManager _audioManager;

    private Collider2D _collider;

    #region Events Declaration
    public event Action OnCharacterActivated;
    #endregion Events Declaration

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    #region Zenject
    [Inject]
    private void Construct(AudioManager audioManager)
    {
        _audioManager = audioManager;
    }
    #endregion Zenject

    public void ChangeColliderState(bool isActive)
    {
        _collider.enabled = isActive;
    }

    public void Interact()
    {
        characterVFXManager.SetAnimationState_GetKey();
        _audioManager.PlayVoicesAudio_KidGivesKey();
        OnCharacterActivated?.Invoke();
    }
}
