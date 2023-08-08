using System.Collections;
using UnityEngine;

public class OldManMovementManager_Backyard : MonoBehaviour
{
    [Header("Movement Data")]
    [Space]
    [SerializeField] private float movementDelta = 5f;
    [SerializeField] private float movementSpeed = 1f;
    [Header("Delays")]
    [Space]
    [SerializeField] private float moveToAnotherPointDelay = 1f;

    private bool canMove = false;

    private Vector3 borderLeftPos = new Vector3();
    private Vector3 startPos = new Vector3();

    private void Start()
    {
        borderLeftPos = new Vector3(transform.position.x - movementDelta, transform.position.y, transform.position.z);
        startPos = transform.position;
    }

    [ContextMenu("Move")]
    public void StartMoving()
    {
        canMove = true;

        MoveLeft();
    }

    private IEnumerator MoveLeftCoroutine(System.Action callback)
    {
        yield return new WaitForSeconds(moveToAnotherPointDelay);

        while(canMove && transform.position.x > borderLeftPos.x)
        {
            transform.position = new Vector3(transform.position.x - movementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            yield return new WaitForEndOfFrame();
        }
        
        if(canMove)
        {
            callback.Invoke();
        }
    }

    private IEnumerator MoveRightCoroutine(System.Action callback)
    {
        yield return new WaitForSeconds(moveToAnotherPointDelay);

        while (canMove && transform.position.x < startPos.x)
        {
            transform.position = new Vector3(transform.position.x + movementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            yield return new WaitForEndOfFrame();
        }

        if (canMove)
        {
            callback.Invoke();
        }
    }

    private void MoveLeft()
    {
        // set animation
        StartCoroutine(MoveLeftCoroutine(MoveRight));
    }

    private void MoveRight()
    {
        // set animation
        StartCoroutine(MoveRightCoroutine(MoveLeft));
    }
}
