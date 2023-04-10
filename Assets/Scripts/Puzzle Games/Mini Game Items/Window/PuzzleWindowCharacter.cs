using UnityEngine;
using System;

public class PuzzleWindowCharacter : MonoBehaviour, Iinteractable
{
    [Header("Internal References")]
    [Space]
    [SerializeField] private WindowCharacterVFXManager characterVFXManager;

    private Collider2D _collider;

    #region Events Declaration
    public event Action OnCharacterActivated;
    #endregion Events Declaration

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    public void ChangeColliderState(bool isActive)
    {
        _collider.enabled = isActive;
    }

    public void Interact()
    {
        characterVFXManager.SetAnimationState_GetKey();
        OnCharacterActivated?.Invoke();
    }
}
