using UnityEngine;

public class PlayerMoveManager : MonoBehaviour
{
    [Header("Move Data")]
    [Space]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float horizontalMoveTreshold = 0.1f;

    private float horizontalMove = 0f;

    private bool canCheckInput = false;

    [SerializeField] private Joystick joystick;

    private void Update()
    {
        if(canCheckInput)
        {
            horizontalMove = joystick.Horizontal * moveSpeed;

            if (joystick.Horizontal >= horizontalMoveTreshold)
            {
                horizontalMove = moveSpeed;
            }
            else if (joystick.Horizontal <= -horizontalMoveTreshold)
            {
                horizontalMove = -moveSpeed;
            }
            else
            {
                horizontalMove = 0f;
            }

            transform.position = new Vector3(transform.position.x + horizontalMove, transform.position.y, transform.position.z);
        }
    }

    public void ChangeCheckingInputState(bool canCheck)
    {
        canCheckInput = canCheck;
    }
}
