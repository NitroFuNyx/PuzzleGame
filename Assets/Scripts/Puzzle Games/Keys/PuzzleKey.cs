using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleKey : PuzzleGameItemInteractionHandler
{
    [Header("Key Data")]
    [Space]
    [SerializeField] private int keyIndex = 0;

    private bool collected = false;

    public override void Interact()
    {
        if(!collected)
        {
            collected = true;
            Debug.Log($"Save Key {keyIndex}");
        }
    }
}
