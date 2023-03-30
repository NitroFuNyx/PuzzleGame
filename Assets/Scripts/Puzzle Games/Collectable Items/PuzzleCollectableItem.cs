using UnityEngine;
using Zenject;

public abstract class PuzzleCollectableItem : MonoBehaviour, Iinteractable
{
    [Header("Item Data")]
    [Space]
    [SerializeField] protected PuzzleGameKitchenItems item;

    protected PuzzleGamesEnvironmentsHolder _puzzleGamesEnvironmentsHolder;

    public void Interact()
    {
        InteractOnTouch();
    }

    #region Zenject
    [Inject]
    private void Construct(PuzzleGamesEnvironmentsHolder puzzleGamesEnvironmentsHolder)
    {
        _puzzleGamesEnvironmentsHolder = puzzleGamesEnvironmentsHolder;
    }
    #endregion Zenject

    public abstract void InteractOnTouch();
}
