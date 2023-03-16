using System.Collections.Generic;
using UnityEngine;

public abstract class KitchenMiniGameItem : MonoBehaviour
{
    [Header("Item Data")]
    [Space]
    [SerializeField] protected KitchenMiniGameItems itemType;
    [Header("Sprites")]
    [Space]
    [SerializeField] private List<Sprite> coinsSpritesList = new List<Sprite>();

    protected SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        SetItemSprite();
    }

    protected void SetItemSprite()
    {
        int index = Random.Range(0, coinsSpritesList.Count);
        spriteRenderer.sprite = coinsSpritesList[index];
    }

    public abstract void OnInteractionWithPlayer_ExecuteReaction(); 
}
