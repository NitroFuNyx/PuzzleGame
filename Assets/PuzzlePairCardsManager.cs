using System.Collections.Generic;
using UnityEngine;

public class PuzzlePairCardsManager : MonoBehaviour
{
    [SerializeField] private List<PuzzlePairCardsItem> puzzleCardsList;
    
    private bool _isPlayerOpenedACard;
    
    private void Start()
    {

        foreach (var balloon in puzzleCardsList)
        {
            balloon.OnButtonClick += OpenCard;
        }
    }

    private void OnDestroy()
    {
        foreach (var balloon in puzzleCardsList)
        {
            balloon.OnButtonClick -= OpenCard;
        }
    }

    private void OpenCard(PuzzlePairCardsItem item)
    {
        
    }

}
