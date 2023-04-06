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

    public void ChangeCameraUnitSize(Transform target, float time, Action OnCameraMovementComplete)
    {
        Vector3 updatedTargetVector = new Vector3(target.position.x, target.position.y, mainCamera.transform.position.z);
        StartCoroutine(ChangeCameraSizeCoroutine());
        mainCamera.transform.DOMove(updatedTargetVector, time).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            OnCameraMovementComplete?.Invoke();
        });
    }

    private IEnumerator ChangeCameraSizeCoroutine()
    {
        float changeAmount = cameraSizeChangeDelta;

        while ((mainCamera.orthographicSize - cameraSizeNearPuzzleWindow) >= 0.1f)
        {
            mainCamera.orthographicSize -= changeAmount;
            yield return new WaitForEndOfFrame();
        }

        //if (mainCamera.orthographicSize > cameraSizeNearPuzzleWindow)
        //{
        //    changeAmount = -cameraSizeChangeDelta;
        //}

        //while(Mathf.Abs(mainCamera.orthographicSize - cameraSizeNearPuzzleWindow) <= 0.1f)
        //{
        //    mainCamera.orthographicSize += changeAmount;
        //    yield return new WaitForEndOfFrame();
        //}
    }
}
