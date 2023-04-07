using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class PuzzleButton : MonoBehaviour, Iinteractable
{
    [Header("Item Type")]
    [Space]
    [SerializeField] private PuzzleGameKitchenMiniGames gameType;
    [Header("Key Data")]
    [Space]
    [SerializeField] private PuzzleKey key;
    [SerializeField] private PuzzleKeyContainer keyContainerComponent;

    private bool buttonPressed = false;
    [SerializeField] private bool containsKey = true;
    private PuzzleGameUI _puzzleGameUI;

    private BoxCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
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

        _puzzleGameUI.TogglePuzzleGame.OnCharacterAnimationFinished += CharacterAnimationFinished_ExecuteReaction;
        
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

        _puzzleGameUI.TogglePuzzleGame.OnCharacterAnimationFinished -= CharacterAnimationFinished_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(PuzzleGameUI puzzleGameUI)
    {
        _puzzleGameUI = puzzleGameUI;
    }
    #endregion Zenject

    public void Interact()
    {        
        buttonPressed = !buttonPressed;

        if (buttonPressed && containsKey && !key.gameObject.activeInHierarchy)
        {
            _puzzleGameUI.ShowMiniGamePanel(gameType);
        }
    }

    public void SetButtonActivation(bool isActive)
    {
        _collider.enabled = isActive;
    }

    private void KeyCollected_ExecuteReaction()
    {
        containsKey = false;
        key.ChangeKeySimulattionState(false);
    }

    private void CollectedItemsDataLoaded_ExecuteReaction(List<PuzzleGameKitchenItems> collectedItemsList, List<PuzzleGameKitchenItems> usedItemsList)
    {
        if (key != null && (collectedItemsList.Contains(key.Item) || usedItemsList.Contains(key.Item)))
        {
            containsKey = false;
            key.ChangeKeySimulattionState(false);
        }
    }

    private void CharacterAnimationFinished_ExecuteReaction()
    {
        key.gameObject.SetActive(true);
        key.ChangeKeySimulattionState(true);
    }

    private IEnumerator SetStartSettingsCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        if (key)
        {
            key.gameObject.SetActive(false);
        }
    }
}
