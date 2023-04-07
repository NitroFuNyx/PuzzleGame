using UnityEngine;
using DG.Tweening;
using Zenject;

public class PuzzleKitchenBook : ButtonInteractionHandler
{
    [Header("Item Type Data")]
    [Space]
    [SerializeField] private PuzzleGameKitchenBooks bookType;
    [Header("Scale Data")]
    [Space]
    [SerializeField] private Vector3 maxScale = new Vector3(1.2f, 1.2f, 1.2f);
    [SerializeField] private float bookScaleDuration = 0.5f;

    private PuzzleKitchenBooksPositionsManager _booksPositionsManager;

    private PuzzleKitchenBookShelf currentShelf;

    public PuzzleGameKitchenBooks BookType { get => bookType; }
    public PuzzleKitchenBookShelf CurrentShelf { get => currentShelf; set => currentShelf = value; }

    #region Zenject
    [Inject]
    private void Construct(PuzzleKitchenBooksPositionsManager puzzleKitchenBooksPositionsManager)
    {
        _booksPositionsManager = puzzleKitchenBooksPositionsManager;
    }
    #endregion Zenject

    public void UpdateShelf(PuzzleKitchenBookShelf shelf)
    {
        currentShelf = shelf;
    }

    public override void ButtonActivated()
    {
        if(_booksPositionsManager.CanCheckBooksInput)
        {
            _booksPositionsManager.SelectBookForMoving(this);
        }
    }

    public void ScaleToMax()
    {
        transform.DOScale(maxScale, bookScaleDuration).SetEase(Ease.InOutBack);
    }

    public void ScaleToStandart()
    {
        transform.DOScale(Vector3.one, bookScaleDuration).SetEase(Ease.InOutBack);
    }
}
