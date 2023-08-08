using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzlePairCardsItem : MonoBehaviour
{
    [SerializeField] private PuzzlePairCards cardPair;
    [SerializeField] private Button button;
    [SerializeField] private Sprite cardFace;
    [SerializeField] private Sprite cardBack;

    [SerializeField] private Image cardImage;
    public RectTransform rectTransform;

    public event Action<PuzzlePairCardsItem> OnButtonClick;

    private bool _isCardOpened;

    public PuzzlePairCards CardPair => cardPair;

    public bool IsCardOpened => _isCardOpened;

    public void SwitchBalloonActivation(bool status)
    {
        gameObject.SetActive(status);

    }
    
    private void Start()
    {
        button.onClick.AddListener(BalloonClicked);
    }

    private void OnDestroy()
    {

    }

    private void OnPuzzleRestart_ExecuteReaction()
    {
        SwitchBalloonActivation(true);
    }

    private void BalloonClicked()
    {
        OnButtonClick?.Invoke(this);

    }

    public void OpenCard()
    {
        cardImage.sprite = cardFace;
        _isCardOpened = true;
    }
    public void CloseCard()
    {
        cardImage.sprite = cardBack;
        _isCardOpened = false;
    }
}
