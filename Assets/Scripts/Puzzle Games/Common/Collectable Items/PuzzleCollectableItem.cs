using UnityEngine;
using Zenject;

public abstract class PuzzleCollectableItem : MonoBehaviour, Iinteractable
{
    [Header("Item Data")]
    [Space]
    [SerializeField] private PuzzleGameCollectableItems item;

    protected PuzzleGamesEnvironmentsHolder _puzzleGamesEnvironmentsHolder;
    protected AudioManager _audioManager;

    public PuzzleGameCollectableItems Item { get => item; }

    public void Interact()
    {
        InteractOnTouch();
        //_audioManager.PlaySFXSound_PickUpKey();
    }

    #region Zenject
    [Inject]
    private void Construct(PuzzleGamesEnvironmentsHolder puzzleGamesEnvironmentsHolder, AudioManager audioManager)
    {
        _puzzleGamesEnvironmentsHolder = puzzleGamesEnvironmentsHolder;
        _audioManager = audioManager;
    }
    #endregion Zenject

    public abstract void InteractOnTouch();
}
