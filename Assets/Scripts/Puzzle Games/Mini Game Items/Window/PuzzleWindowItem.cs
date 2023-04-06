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
    private PuzzleGameItem_MiniGameModeStarter gameStarter;
    private PuzzleGameUI _puzzleGameUI;

    private Collider2D _collider;

    private float returnToMainModeDelay = 0.3f;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        gameStarter = GetComponent<PuzzleGameItem_MiniGameModeStarter>();
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
    private void Construct(CameraManager cameraManager, PuzzleGamesEnvironmentsHolder puzzleGamesEnvironmentsHolder, PuzzleGameUI puzzleGameUI)
    {
        _cameraManager = cameraManager;
        _environmentsHolder = puzzleGamesEnvironmentsHolder;
        _puzzleGameUI = puzzleGameUI;
    }
    #endregion Zenject

    public void WindowInteraction_ExecuteReaction()
    {
        _collider.enabled = false;
        _cameraManager.MoveCameraToWindow(this.transform, cameraMoveDurtation, CameraMovementToWindowComplete_ExecuteReaction);
    }

    private void CameraMovementToWindowComplete_ExecuteReaction()
    {
        _environmentsHolder.CurrentlyActiveGame.InputManager.ChangeCheckInputState(true);
        _environmentsHolder.CurrentlyActiveGame.InputManager.CanMoveCamera = false;
        windowCharacter.ChangeColliderState(true);
    }

    private void CameraMovementToStartPosComplete_ExecuteReaction()
    {
        _environmentsHolder.CurrentlyActiveGame.InputManager.ChangeCheckInputState(true);
        _environmentsHolder.CurrentlyActiveGame.InputManager.CanMoveCamera = true;
        _puzzleGameUI.ShowMainModePanel();
    }

    private void WindowCharacterActivated_ExecuteReaction()
    {
        windowCharacter.ChangeColliderState(false);
        _environmentsHolder.CurrentlyActiveGame.InputManager.ChangeCheckInputState(false);
        _environmentsHolder.CurrentlyActiveGame.InputManager.CanMoveCamera = true;
        gameStarter.ShowKey();
        StartCoroutine(ReturnToMainModeCoroutine());
    }

    private IEnumerator ReturnToMainModeCoroutine()
    {
        yield return new WaitForSeconds(returnToMainModeDelay);
        _cameraManager.ReturnCameraToStartPos(cameraMoveDurtation, CameraMovementToStartPosComplete_ExecuteReaction);
    }
}
