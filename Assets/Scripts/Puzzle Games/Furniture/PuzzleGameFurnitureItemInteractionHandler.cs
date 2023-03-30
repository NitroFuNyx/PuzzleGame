using UnityEngine;

public abstract class PuzzleGameFurnitureItemInteractionHandler : MonoBehaviour, Iinteractable
{
    [Header("Item Data")]
    [Space]
    [SerializeField] protected bool containsKey = false;
    [Header("Keys")]
    [Space]
    [SerializeField] protected PuzzleKey key;

    public void Interact()
    {
        InteractOnTouch();
    }

    public abstract void InteractOnTouch();
}
