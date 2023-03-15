using UnityEngine;
using System;

public class PlayerMoveManager : MonoBehaviour
{
    [Header("Move Data")]
    [Space]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float horizontalMoveTreshold = 0.1f;

    private float horizontalMove = 0f;

    private bool canCheckInput = false;
    private bool moving = false;

    [SerializeField] private Joystick joystick;

    #region Events Declaration
    public event Action OnCharacterStartMoving;
    public event Action OnCharacterStopMoving;
    #endregion Events Declaration

    private void Update()
    {
        if(canCheckInput)
        {
            horizontalMove = joystick.Horizontal * moveSpeed;
  
            if (joystick.Horizontal >= horizontalMoveTreshold)
            {
                horizontalMove = moveSpeed;
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (joystick.Horizontal <= -horizontalMoveTreshold)
            {
                horizontalMove = -moveSpeed;
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                horizontalMove = 0f;
            }

            if(Input.touchCount > 0)
            {
                if(Input.GetTouch(0).phase == TouchPhase.Moved && !moving)
                {
                    moving = true;
                    OnCharacterStartMoving?.Invoke();
                }
                else if(Input.GetTouch(0).phase == TouchPhase.Ended && moving)
                {
                    moving = false;
                    OnCharacterStopMoving?.Invoke();
                }
            }

            transform.position = new Vector3(transform.position.x + horizontalMove, transform.position.y, transform.position.z);
        }
    }

    public void ChangeCheckingInputState(bool canCheck)
    {
        canCheckInput = canCheck;
    }
}
