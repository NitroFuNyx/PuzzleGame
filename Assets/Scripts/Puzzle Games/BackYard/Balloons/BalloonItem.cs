using System;
using UnityEngine;
using UnityEngine.UI;

public class BalloonItem : MonoBehaviour
{
    [SerializeField] private Image itemImage;

    [SerializeField]private PuzzleBalloonsGameItemsColors _color;
    [SerializeField] private PuzzleGameBalloonsManager _manager;
    public PuzzleBalloonsGameItemsColors ItemColor => _color;
    [SerializeField] private Button button;
    public RectTransform rectTransform;
    public event Action<BalloonItem> OnButtonClick;
    public void SwitchBalloonActivation(bool status)
    {
        gameObject.SetActive(status);

    }
    
    private void Start()
    {
        _manager.OnPuzzleRestart += OnPuzzleRestart_ExecuteReaction;
        button.onClick.AddListener(BalloonClicked);
    }

    private void OnDestroy()
    {
        _manager.OnPuzzleRestart -= OnPuzzleRestart_ExecuteReaction;

    }

    private void OnPuzzleRestart_ExecuteReaction()
    {
        SwitchBalloonActivation(true);
    }

    private void BalloonClicked()
    {
        OnButtonClick?.Invoke(this);
        
        
    }
}
