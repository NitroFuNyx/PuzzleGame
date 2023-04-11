using UnityEngine;
using System.Collections.Generic;

public class BookshelfImage : MonoBehaviour
{
    [Header("Books Shelves")]
    [Space]
    [SerializeField] private List<PuzzleKitchenBookShelf> booksShelvesList = new List<PuzzleKitchenBookShelf>();

    public List<PuzzleKitchenBookShelf> BooksShelvesList { get => booksShelvesList; }
}
