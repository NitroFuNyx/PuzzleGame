using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGameItem_SmallDrawer : PuzzleGameFurnitureItemInteractionHandler
{
    [Header("Door Data")]
    [Space]
    [SerializeField] private SpriteRenderer door;
    [Header("Sprites")]
    [Space]
    [SerializeField] private Sprite closedDoorSprite;
    [SerializeField] private Sprite openDoorSprite;

    private bool isOpen = false;
    private bool keyIsCollected = false;

    private void Awake()
    {
        if(containsKey && key != null)
        {
            key.gameObject.SetActive(false);
        }
    }

    public override void Interact()
    {
        isOpen = !isOpen;

        if(!isOpen)
        {
            door.sprite = closedDoorSprite;
        }
        else
        {
            if(containsKey)
            {
                key.gameObject.SetActive(true);
            }
            door.sprite = openDoorSprite;
        }
    }
}
