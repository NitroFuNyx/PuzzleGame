using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using Zenject;

public class TogglePuzzleGame : MonoBehaviour
{
    [SerializeField] private Toggle redButton;
    [SerializeField] private Animator ded;
    private PuzzleGameUI _puzzleGameUI;

    private CameraManager _cameraManager;
    private PuzzleGamesEnvironmentsHolder _puzzleGamesEnvironmentsHolder;

    #region Events Declaration
    public event Action OnCharacterAnimationFinished;
    #endregion Events Declaration

    #region Zenject
    [Inject]
    private void Construct(PuzzleGameUI puzzleGameUI,CameraManager cameraManager,PuzzleGamesEnvironmentsHolder puzzleGamesEnvironmentsHolder)
    {
        _puzzleGamesEnvironmentsHolder = puzzleGamesEnvironmentsHolder;
        _cameraManager = cameraManager;
        _puzzleGameUI = puzzleGameUI;
    }
    #endregion Zenject
    
    private void Start()
    {
        redButton.onValueChanged.AddListener(delegate {
            ForbidToggling(redButton);
        });
    }

    private void OnDisable()
    {
        redButton.onValueChanged.RemoveListener(delegate {
            ForbidToggling(redButton);
        });
    }

    public void ResetGame()
    {
        ded.SetTrigger(OldManAnimations.Reset);
        redButton.interactable = true;
        
    }

    private void ForbidToggling(bool toggleStatus)
    {
        Debug.Log("Toggle  works - "+ toggleStatus);
        if (toggleStatus)
        {
            redButton.interactable = false;
            redButton.isOn = false;
            StartCoroutine(SpawnAMan());

        }
    }
     
    private IEnumerator SpawnAMan()
    {
        yield return new WaitForSeconds(1f);
        _puzzleGameUI.ShowMainModePanel();
        yield return new WaitForSeconds(0.25f);
        _cameraManager.CameraMoveTo(ded.transform,1.5f);
        yield return new WaitForSeconds(1.5f);
        ded.SetTrigger("Appear");
        yield return new WaitForSeconds(3.5f);
        _puzzleGamesEnvironmentsHolder.CurrentlyActiveGame.InputManager.ChangeCheckInputState(true);
        OnCharacterAnimationFinished?.Invoke();
    }
}
