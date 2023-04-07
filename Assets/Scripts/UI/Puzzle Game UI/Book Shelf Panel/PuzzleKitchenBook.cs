using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PuzzleKitchenBook : ButtonInteractionHandler
{
    [Header("Item Type Data")]
    [Space]
    [SerializeField] private PuzzleGameKitchenBooks bookType;

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
}