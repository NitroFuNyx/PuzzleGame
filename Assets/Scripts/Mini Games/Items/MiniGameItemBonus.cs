using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameItemBonus : MonoBehaviour
{
    [Header("Item Data")]
    [Space]
    [SerializeField] private int coinBobus = 1;
    [Header("Sprites")]
    [Space]
    [SerializeField] private List<Sprite> coinsSpritesList = new List<Sprite>();

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        SetItemSprite();
    }

    public void SetItemSprite()
    {
        int index = Random.Range(0, coinsSpritesList.Count);
        spriteRenderer.sprite = coinsSpritesList[index];
    }
}
