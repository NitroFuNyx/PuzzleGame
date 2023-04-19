using System.Collections;
using UnityEngine;
using Zenject;

public class PuzzleInputManager : MonoBehaviour
{
    [Header("Move Data")]
    [Space]
    [SerializeField] private float moveSpeed = 10f;
    [Header("Clamp Parameters")]
    [Space]
    [SerializeField] private float clampXUnitMin = 0f;
    [SerializeField] private float clampXUnitMax = 40f;
    [Header("Joystick")]
    [Space]
    [SerializeField] private Joystick joystick;

    private CurrentGameManager _currentGameManager;
    private AudioManager _audioManager;

    private float horizontalMove = 0f;
    private float checkInteractDelay = 0.1f;

    private bool canCheckInput = false;
    private bool canMoveCamera = true;

    public bool CanMoveCamera { get => canMoveCamera; set => canMoveCamera = value; }

    private void Update()
    {
        if (canCheckInput)
        {
            if(canMoveCamera)
            {
                MoveCamera();
            }

            CreateRaycast();
        }
    }

    #region Zenject
    [Inject]
    private void Construct(CurrentGameManager currentGameManager, AudioManager audioManager)
    {
        _currentGameManager = currentGameManager;
        _audioManager = audioManager;
    }
    #endregion Zenject

    public void ChangeCheckInputState(bool canCheck)
    {
        canCheckInput = canCheck;
    }

    private void MoveCamera()
    {
        horizontalMove = -joystick.Horizontal * moveSpeed * Time.deltaTime;

        float clampedPosX = Mathf.Clamp(Camera.main.transform.position.x + (horizontalMove), clampXUnitMin, clampXUnitMax);

        Camera.main.transform.position = new Vector3(clampedPosX, Camera.main.transform.position.y, Camera.main.transform.position.z);
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

                    if (hit.collider.TryGetComponent(out Iinteractable item))
                    {
                        //item.Interact();
                        StartCoroutine(CheckInteractionCoroutine(item, hit.collider));
                    }
                }
            }
        }
    }

    private IEnumerator CheckInteractionCoroutine(Iinteractable item, Collider2D collider)
    {
        yield return new WaitForSeconds(checkInteractDelay);
        if(!_currentGameManager.PuzzleUIButtonPressed)
        {
            item.Interact();
            if(collider.TryGetComponent(out PuzzleKitchenLamp lamp))
            {
                _audioManager.PlaySFXSound_PuzzleLampInteraction();
            }
            else
            {
                _audioManager.PlaySFXSound_PuzzleItemInteraction();
            }
        }
    }
}
