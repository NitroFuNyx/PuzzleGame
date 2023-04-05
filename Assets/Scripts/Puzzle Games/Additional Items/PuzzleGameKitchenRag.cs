using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGameKitchenRag : MonoBehaviour, Iinteractable
{
    [Header("Internal References")]
    [Space]
    [SerializeField] private PuzzleKey key;
    [SerializeField] private PuzzleKeyContainer keyContainerComponent;
    [Header("Sprites")]
    [Space]
    [SerializeField] private Sprite ragStandartSprite;
    [SerializeField] private Sprite ragMovedSprite;

    private SpriteRenderer spriteRenderer;

    private bool ragMoved = false;
    private bool containsKey = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (TryGetComponent(out PuzzleClueHolder clueHolder))
        {
            clueHolder.ClueIndex = key.KeyIndex;
        }
        if (key)
        {
            key.OnKeyCollected += KeyCollected_ExecuteReaction;
        }
        if (keyContainerComponent)
        {
            keyContainerComponent.OnCollectedItemsDataLoaded += CollectedItemsDataLoaded_ExecuteReaction;
        }
        StartCoroutine(SetStartSettingsCoroutine());
    }

    private void OnDestroy()
    {
        if (key)
        {
            key.OnKeyCollected -= KeyCollected_ExecuteReaction;
        }
        if (keyContainerComponent)
        {
            keyContainerComponent.OnCollectedItemsDataLoaded -= CollectedItemsDataLoaded_ExecuteReaction;
        }
    }

    public void Interact()
    {
        ragMoved = !ragMoved;

        if(ragMoved)
        {
            spriteRenderer.sprite = ragMovedSprite;
            if (containsKey)
            {
                key.gameObject.SetActive(true);
            }
        }
        else
        {
            spriteRenderer.sprite = ragStandartSprite;
            if(key.gameObject.activeInHierarchy)
            {
                key.gameObject.SetActive(false);
            }
        }
    }

    private void KeyCollected_ExecuteReaction()
    {
        containsKey = false;
    }

    private void CollectedItemsDataLoaded_ExecuteReaction(List<PuzzleGameKitchenItems> collectedItemsList)
    {
        if (key != null && collectedItemsList.Contains(key.Item))
        {
            containsKey = false;
        }
    }

    private IEnumerator SetStartSettingsCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        key.gameObject.SetActive(false);
    }
}
