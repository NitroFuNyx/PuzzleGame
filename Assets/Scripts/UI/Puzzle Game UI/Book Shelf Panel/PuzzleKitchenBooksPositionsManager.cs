using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PuzzleKitchenBooksPositionsManager : MonoBehaviour
{
    [Header("Books Shelves")]
    [Space]
    [SerializeField] private List<PuzzleKitchenBookShelf> booksShelvesList = new List<PuzzleKitchenBookShelf>();
    [Header("Current Data")]
    [Space]
    [SerializeField] private PuzzleKitchenBook firstSelectedBook;
    [SerializeField] private PuzzleKitchenBook secondSelectedBook;
    [SerializeField] private bool canCheckBooksInput = false;

    private Action OnGameComplete;

    public bool CanCheckBooksInput { get => canCheckBooksInput; private set => canCheckBooksInput = value; }

    public void StartGame(Action OnBookshelfGameComplete)
    {
        canCheckBooksInput = true;
        OnGameComplete = OnBookshelfGameComplete;
    }

    public void SelectBookForMoving(PuzzleKitchenBook book)
    {
        if (firstSelectedBook == null)
        {
            firstSelectedBook = book;
        }
        else
        {
            if(book.CurrentShelf != firstSelectedBook.CurrentShelf)
            secondSelectedBook = book;
        }

        if (firstSelectedBook && secondSelectedBook)
        {
            canCheckBooksInput = false;

            Vector3 firstBookStartPos = firstSelectedBook.transform.localPosition;
            Vector3 secondBookStartPos = firstSelectedBook.transform.localPosition;

            firstSelectedBook.transform.SetParent(secondSelectedBook.CurrentShelf.transform);
            secondSelectedBook.transform.SetParent(firstSelectedBook.CurrentShelf.transform);

            firstSelectedBook.transform.localPosition = secondBookStartPos;
            secondSelectedBook.transform.localPosition = firstBookStartPos;

            for(int i = 0; i < booksShelvesList.Count; i++)
            {
                booksShelvesList[i].UpdateBooksData();
            }

            if (CheckShelvesState())
            {
                // win
                Debug.Log($"Win");
            }
            else
            {
                firstSelectedBook = null;
                secondSelectedBook = null;
                canCheckBooksInput = true;
            }
        }
    }

    private bool CheckShelvesState()
    {
        bool allShelvesBooksAreCorrect = true;

        for (int i = 0; i < booksShelvesList.Count; i++)
        {
            if (booksShelvesList[i].AllBooksInShelfAreCorrect == false)
            {
                allShelvesBooksAreCorrect = false;
                break;
            }
        }

        return allShelvesBooksAreCorrect;
    }
}
