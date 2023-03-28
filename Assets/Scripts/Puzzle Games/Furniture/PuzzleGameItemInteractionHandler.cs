using UnityEngine;

public abstract class PuzzleGameItemInteractionHandler : MonoBehaviour
{
    [Header("Item Data")]
    [Space]
    [SerializeField] protected bool containsKey = false;
    [Header("Keys")]
    [Space]
    [SerializeField] protected PuzzleKey key;

    public abstract void Interact();
}
