using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button),typeof(Image))]
public class PuzzleGameCake : MonoBehaviour
{
    [SerializeField] private List<Sprite> cakeShapeList;

    [Header("References")]
    [Space]
    [SerializeField] private Image UICakeImage;
    [SerializeField] private Image PuzzleCakeOnTable;
    [SerializeField] private Button button;

    
    public event Action OnPuzzleFinish;

    private int _clicksAmount;
    private void OnEnable()
    {
        if(PuzzleCakeOnTable==null)
            Debug.LogError("Cake on table is still not assigned!");
        button.onClick.AddListener(SwitchCakeShape);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(SwitchCakeShape);
        
    }

    private void SwitchCakeShape()
    {
        if (_clicksAmount >= 9)
        {
            OnPuzzleFinish?.Invoke();
            return;
        }
        UICakeImage.sprite = cakeShapeList[_clicksAmount];
        _clicksAmount++;
    }
}
