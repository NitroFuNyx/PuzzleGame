using UnityEngine;
using Zenject;

public abstract class PuzzleCollectableItem : MonoBehaviour
{
    [Header("Item Data")]
    [Space]
    [SerializeField] protected PuzzleGameKitchenItems item;

    protected PuzzleGamesEnvironmentsHolder _puzzleGamesEnvironmentsHolder;

    #region Zenject
    [Inject]
    private void Construct(PuzzleGamesEnvironmentsHolder puzzleGamesEnvironmentsHolder)
    {
        _puzzleGamesEnvironmentsHolder = puzzleGamesEnvironmentsHolder;
    }
    #endregion Zenject

    public abstract void Interact();
}
