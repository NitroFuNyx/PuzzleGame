using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Start Data")]
    [Space]
    [SerializeField] private Vector3 mainCameraStartPos = new Vector3(0f, 0f, -10f);

    private void Awake()
    {
        SetCameraStartPos();
    }

    public void SetCameraStartPos()
    {
        Camera.main.transform.position = mainCameraStartPos;
    }
}
