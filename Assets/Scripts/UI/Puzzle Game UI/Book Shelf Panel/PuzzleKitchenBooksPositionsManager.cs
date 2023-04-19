using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

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
    [Header("Prefabs")]
    [Space]
    [SerializeField] private BookshelfImage bookshelfImagePrefab;
    [SerializeField] private PuzzleGame_MiniGameModePanel bookshelfImageParent;

    private AudioManager _audioManager;

    private BookshelfImage bookShelf;

    private Action OnGameComplete;

    private float hidePanelDelay = 0.5f;

    public bool CanCheckBooksInput { get => canCheckBooksInput; private set => canCheckBooksInput = value; }

    #region Zenject
    [Inject]
    private void Construct(AudioManager audioManager)
    {
        _audioManager = audioManager;
    }
    #endregion Zenject

    public void StartGame(Action OnBookshelfGameComplete)
    {
        canCheckBooksInput = true;
        OnGameComplete = OnBookshelfGameComplete;
        //SpawnBookshelfImage();
    }

    public void SelectBookForMoving(PuzzleKitchenBook book)
    {
        if (firstSelectedBook == null)
        {
            firstSelectedBook = book;
            firstSelectedBook.ScaleToMax();
        }
        else
        {
            //if(book.CurrentShelf != firstSelectedBook.CurrentShelf)
            secondSelectedBook = book;
            secondSelectedBook.ScaleToMax();
        }

        if (firstSelectedBook && secondSelectedBook)
        {
            canCheckBooksInput = false;

            Vector3 firstBookStartPos = firstSelectedBook.transform.localPosition;
            Vector3 secondBookStartPos = secondSelectedBook.transform.localPosition;

            firstSelectedBook.transform.SetParent(secondSelectedBook.CurrentShelf.transform);
            secondSelectedBook.transform.SetParent(firstSelectedBook.CurrentShelf.transform);

            _audioManager.PlaySFXSound_PickUpKey();

            firstSelectedBook.transform.localPosition = secondBookStartPos;
            secondSelectedBook.transform.localPosition = firstBookStartPos;

            for(int i = 0; i < booksShelvesList.Count; i++)
            {
                booksShelvesList[i].UpdateBooksData();
            }

            StartCoroutine(CheckBookShelvesResultsCoroutine());
        }
    }

    public void ResetGame()
    {
        canCheckBooksInput = false;
        firstSelectedBook = null;
        secondSelectedBook = null;

        //for(int i = 0; i < booksShelvesList.Count; i++)
        //{
        //    booksShelvesList[i].ResetShelf();
        //}
    }

    public void SpawnBookshelfImage(Action OnSpawnComplete, Vector3 scale)
    {
        if(bookShelf != null)
        {
            Destroy(bookShelf.gameObject);
        }
        booksShelvesList.Clear();

        bookShelf = Instantiate(bookshelfImagePrefab, Vector3.zero, Quaternion.identity, bookshelfImageParent.transform);
        bookShelf.transform.localPosition = Vector3.zero;
        bookshelfImageParent.PanelMainImage = bookShelf.transform;
        bookShelf.transform.localScale = scale;
        for(int i = 0; i < bookShelf.BooksShelvesList.Count; i++)
        {
            booksShelvesList.Add(bookShelf.BooksShelvesList[i]);
        }
        OnSpawnComplete?.Invoke();
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

    private IEnumerator CheckBookShelvesResultsCoroutine()
    {
        yield return null;
        yield return null;
        yield return null;
        yield return null;

        if (CheckShelvesState())
        {
            firstSelectedBook.ScaleToStandart();
            secondSelectedBook.ScaleToStandart();
            canCheckBooksInput = false;
            yield return new WaitForSeconds(hidePanelDelay);
            OnGameComplete?.Invoke();
        }
        else
        {
            firstSelectedBook.ScaleToStandart();
            secondSelectedBook.ScaleToStandart();

            firstSelectedBook = null;
            secondSelectedBook = null;
            canCheckBooksInput = true;
        }
    }
}
