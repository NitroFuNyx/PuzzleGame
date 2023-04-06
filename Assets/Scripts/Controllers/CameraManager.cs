using System;
using System.Collections;
using UnityEngine;
using Zenject;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    [Header("Start Data")]
    [Space]
    [SerializeField] private Vector3 mainCameraStartPos = new Vector3(0f, 0f, -10f);
    [SerializeField] private float cameraStartSize = 5f;
    [Header("Camera Size Data")]
    [Space]
    [SerializeField] private float cameraSizeChangeDelta = 0.1f;
    [SerializeField] private float cameraSizeNearPuzzleWindow = 3f;

    private Camera mainCamera;
    private PuzzleGamesEnvironmentsHolder _puzzleGamesEnvironmentsHolder;

    private float cameraSizeTreshold = 0.05f;

    [Inject]
    private void InjectDependencies(PuzzleGamesEnvironmentsHolder puzzleGamesEnvironmentsHolder)
    {
        _puzzleGamesEnvironmentsHolder = puzzleGamesEnvironmentsHolder;
    }

    private void Awake()
    {
        mainCamera = Camera.main;
        SetCameraStartPos();
    }

    public void CameraMoveTo(Transform target, float time)
    {
        _puzzleGamesEnvironmentsHolder.CurrentlyActiveGame.InputManager.ChangeCheckInputState(false);
        mainCamera.transform.DOMoveX(target.position.x,time).SetEase(Ease.InOutSine);
    }

    public void SetCameraStartPos()
    {
        mainCamera.transform.position = mainCameraStartPos;
    }

    public void MoveCameraToWindow(Transform target, float time, Action OnCameraMovementComplete)
    {
        Vector3 updatedTargetVector = new Vector3(target.position.x, target.position.y, mainCamera.transform.position.z);
        StartCoroutine(DecreaseCameraSize());
        mainCamera.transform.DOMove(updatedTargetVector, time).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            OnCameraMovementComplete?.Invoke();
        });
    }

    public void ReturnCameraToStartPos(float time, Action OnCameraMovementComplete)
    {
        Vector3 updatedTargetVector = new Vector3(mainCamera.transform.position.x, mainCameraStartPos.y, mainCamera.transform.position.z);
        StartCoroutine(IncreaseCameraSize());
        mainCamera.transform.DOMove(updatedTargetVector, time).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            OnCameraMovementComplete?.Invoke();
        });
    }

    private IEnumerator DecreaseCameraSize()
    {
        float changeAmount = cameraSizeChangeDelta;

        while ((mainCamera.orthographicSize - cameraSizeNearPuzzleWindow) >= cameraSizeTreshold)
        {
            mainCamera.orthographicSize -= changeAmount * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator IncreaseCameraSize()
    {
        float changeAmount = cameraSizeChangeDelta;

        while ((cameraStartSize - mainCamera.orthographicSize) >= cameraSizeTreshold)
        {
            mainCamera.orthographicSize += changeAmount * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
