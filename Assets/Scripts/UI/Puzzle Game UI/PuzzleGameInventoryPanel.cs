using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleGameInventoryPanel : MonoBehaviour
{
    [Header("Inventory Items Cells")]
    [Space]
    [SerializeField] private List<Image> allInventoryCellsList = new List<Image>();
    [SerializeField] private List<Image> freeInventoryCellsList = new List<Image>();

    public void PutItemInInventoryCell(Sprite sprite)
    {
        allInventoryCellsList[0].sprite = sprite;
    }
}
