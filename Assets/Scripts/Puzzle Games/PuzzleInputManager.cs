using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleInputManager : MonoBehaviour
{
    [Header("Move Data")]
    [Space]
    [SerializeField] private float moveSpeed = 10f;
    [Header("Clamp Parameters")]
    [Space]
    [SerializeField] private float clampXUnitMin = 0f;
    [SerializeField] private float clampXUnitMax = 40f;

    private float horizontalMove = 0f;

    private bool canCheckInput = true;

    [SerializeField] private Joystick joystick;

    private void Update()
    {
        //if (canCheckInput)
        //{
            
            horizontalMove = joystick.Horizontal * moveSpeed * Time.deltaTime;

            float clampedPosX = Mathf.Clamp(Camera.main.transform.position.x + (horizontalMove), clampXUnitMin, clampXUnitMax);

            Camera.main.transform.position = new Vector3(clampedPosX, Camera.main.transform.position.y, Camera.main.transform.position.z);
        //}

        CreateRaycast();
    }

    private void CreateRaycast()
    {
        if(Input.touchCount > 0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                RaycastHit2D hit = Physics2D.Raycast(touchPos, transform.forward, 20f);
                Debug.DrawRay(touchPos, transform.forward * 20f, Color.blue);
                if (hit.collider != null)
                {
                    Debug.Log($"{hit.collider.name}");
                    if (hit.collider.TryGetComponent(out PuzzleGameItemInteractionHandler item))
                    {
                        item.Interact();
                    }
                }
            }
        }
    }
}
