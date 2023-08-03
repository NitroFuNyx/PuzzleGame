using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzlePairCardsItem : MonoBehaviour
{
    [SerializeField] private PuzzlePairCards cardPair;
    
    
    
    
    [SerializeField] private Button button;
    public event Action<PuzzlePairCardsItem> OnButtonClick;
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
    
    
}
