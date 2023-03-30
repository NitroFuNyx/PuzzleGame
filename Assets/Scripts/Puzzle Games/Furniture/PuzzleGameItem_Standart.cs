using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class PuzzleGameItem_Standart : PuzzleGameFurnitureItemInteractionHandler
{
    [Header("Scale Data")]
    [Space]
    [SerializeField] private Vector3 scalePunchVector = new Vector3(-0.2f, -0.2f, -0.2f);
    [SerializeField] private int scaleFreequency = 3;
    [SerializeField] private float scaleDuration = 0.5f;

    private bool isAnimating = false;

    public override void CollectedItemsDataLoaded_ExecuteReaction(List<PuzzleGameKitchenItems> collectedItemsList)
    {
        
    }

    public override void InteractOnTouch()
    {
        if(!isAnimating)
        {
            isAnimating = true;
            transform.DOPunchScale(scalePunchVector, scaleDuration, scaleFreequency).OnComplete(() =>
            {
                isAnimating = false;
            });
        }
    }

    public override void KeyCollected_ExecuteReaction()
    {
        
    }
}
