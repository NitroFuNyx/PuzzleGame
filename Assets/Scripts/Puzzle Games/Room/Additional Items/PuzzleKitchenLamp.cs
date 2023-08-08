using UnityEngine;

public class PuzzleKitchenLamp : MonoBehaviour, Iinteractable
{
    [Header("Internal Referemces")]
    [Space]
    [SerializeField] private GameObject lightObject;

    private bool isActivated = false;

    private void Start()
    {
        lightObject.SetActive(false);
    }

    public void Interact()
    {
        isActivated = !isActivated;

        if(isActivated)
        {
            lightObject.SetActive(true);
        }
        else
        {
            lightObject.SetActive(false);
        }
    }
}
