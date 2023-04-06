using DG.Tweening;
using UnityEngine;
using Zenject;

public class CameraManager : MonoBehaviour
{
    [Header("Start Data")]
    [Space]
    [SerializeField] private Vector3 mainCameraStartPos = new Vector3(0f, 0f, -10f);

    private Camera mainCamera;
    private PuzzleGamesEnvironmentsHolder _puzzleGamesEnvironmentsHolder;

    [Inject]
    private void InjectDependencies(PuzzleGamesEnvironmentsHolder puzzleGamesEnvironmentsHolder)
    {
        _puzzleGamesEnvironmentsHolder = puzzleGamesEnvironmentsHolder;
    }


    private void Awake()
    {
        mainCamera = Camera.main;
        mainCamera.transform.position = mainCameraStartPos;
    }

    public void CameraMoveTo(Transform target,float time)
    {
        _puzzleGamesEnvironmentsHolder.CurrentlyActiveGame.InputManager.ChangeCheckInputState(false);
        mainCamera.transform.DOMoveX(target.position.x,time).SetEase(Ease.InOutSine);
    }
}
