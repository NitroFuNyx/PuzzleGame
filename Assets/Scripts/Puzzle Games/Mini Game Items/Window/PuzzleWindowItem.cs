using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PuzzleWindowItem : MonoBehaviour
{
    [Header("Durations")]
    [Space]
    [SerializeField] private float cameraMoveDurtation = 1f;
    [Header("Characters")]
    [Space]
    [SerializeField] private PuzzleWindowCharacter windowCharacter;

    private CameraManager _cameraManager;
    private PuzzleGamesEnvironmentsHolder _environmentsHolder;

    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        windowCharacter.ChangeColliderState(false);
    }

    private void Start()
    {
        windowCharacter.OnCharacterActivated += WindowCharacterActivated_ExecuteReaction;
    }

    private void OnDestroy()
    {
        windowCharacter.OnCharacterActivated -= WindowCharacterActivated_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(CameraManager cameraManager, PuzzleGamesEnvironmentsHolder puzzleGamesEnvironmentsHolder)
    {
        _cameraManager = cameraManager;
        _environmentsHolder = puzzleGamesEnvironmentsHolder;
    }
    #endregion Zenject

    public void WindowInteraction_ExecuteReaction()
    {
        _collider.enabled = false;
        _cameraManager.ChangeCameraUnitSize(this.transform, cameraMoveDurtation, CameraMovementComplete_ExecuteReaction);
    }

    private void CameraMovementComplete_ExecuteReaction()
    {
        _environmentsHolder.CurrentlyActiveGame.InputManager.ChangeCheckInputState(true);
        _environmentsHolder.CurrentlyActiveGame.InputManager.CanMoveCamera = false;
        windowCharacter.ChangeColliderState(true);
    }

    private void WindowCharacterActivated_ExecuteReaction()
    {
        windowCharacter.ChangeColliderState(false);
        _environmentsHolder.CurrentlyActiveGame.InputManager.ChangeCheckInputState(false);
        _environmentsHolder.CurrentlyActiveGame.InputManager.CanMoveCamera = true;
        Debug.Log($"Key");
    }
}
