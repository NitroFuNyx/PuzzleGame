using System;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class SandMountainPuzzleManager : MonoBehaviour,Iinteractable
{
    [SerializeField] private List<Sprite> SandStatesList;

    [Header("References")]
    [Space]
    [SerializeField] private SpriteRenderer objectSpriteRenderer;

    
    public event Action OnPuzzleFinish;

    private int _clicksAmount;
    

    private void SwitchObjectShape()
    {
        if (_clicksAmount >= SandStatesList.Count)
        {
            OnPuzzleFinish?.Invoke();
            return;
        }
        objectSpriteRenderer.sprite = SandStatesList[_clicksAmount];
        _clicksAmount++;
    }

    public void Interact()
    {
        SwitchObjectShape();
    }
}
