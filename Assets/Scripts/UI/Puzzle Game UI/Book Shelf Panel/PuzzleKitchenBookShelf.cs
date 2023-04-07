using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleKitchenBookShelf : MonoBehaviour
{
    [Header("Shelf Books Type Data")]
    [Space]
    [SerializeField] private PuzzleGameKitchenBooks shelfBooksbookType;
    [Header("Books")]
    [Space]
    [SerializeField] private List<PuzzleKitchenBook> booksAtShelfList = new List<PuzzleKitchenBook>();
    [SerializeField] private bool allBooksInShelfAreCorrect = false;

    public PuzzleGameKitchenBooks ShelfBooksbookType { get => shelfBooksbookType; }
    public bool AllBooksInShelfAreCorrect { get => allBooksInShelfAreCorrect; private set => allBooksInShelfAreCorrect = value; }

    private void Start()
    {
        for(int i = 0; i < booksAtShelfList.Count; i++)
        {
            booksAtShelfList[i].UpdateShelf(this);
        }
    }

    public void UpdateBooksData()
    {
        UpdateBooksList();
    }

    private void UpdateBooksList()
    {
        booksAtShelfList.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).TryGetComponent(out PuzzleKitchenBook book))
            {
                booksAtShelfList.Add(book);
                book.UpdateShelf(this);
            }
        }

        CheckBooksType();
    }

    private void CheckBooksType()
    {
        allBooksInShelfAreCorrect = true;

        for(int i = 0; i < booksAtShelfList.Count; i++)
        {
            if (booksAtShelfList[i].BookType != shelfBooksbookType)
            {
                allBooksInShelfAreCorrect = false;
                break;
            }
        }
    }
}
