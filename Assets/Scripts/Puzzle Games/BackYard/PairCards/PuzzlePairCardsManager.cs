using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PuzzlePairCardsManager : MonoBehaviour
{
    [SerializeField] private List<PuzzlePairCardsItem> puzzleCardsList;
    [SerializeField] private float openCardTime;
    private PuzzlePairCardsItem _lastOpenedCard;
    private bool _isPlayerOpenedACard;
    private bool _isCardOpenning;
    private int _pairsSolved;
    public event Action OnGameFinished;
    private void Start()
    {
        ShufflePositions();
        foreach (var balloon in puzzleCardsList)
        {
            balloon.OnButtonClick += OpenCard_ExecuteReaction;
        }
    }

    private void OnDestroy()
    {
        foreach (var balloon in puzzleCardsList)
        {
            balloon.OnButtonClick -= OpenCard_ExecuteReaction;
        }
    }

    private void OpenCard_ExecuteReaction(PuzzlePairCardsItem item)
    {
        StartCoroutine(OpenCard(item));
    }
    private IEnumerator OpenCard(PuzzlePairCardsItem item)
    {
        if (_isCardOpenning) yield break;
        _isCardOpenning = true;
        if (!_isPlayerOpenedACard&&!item.IsCardOpened)
        {
            item.OpenCard();
            _lastOpenedCard = item;
            _isPlayerOpenedACard = true;
        }
        else if(_isPlayerOpenedACard&&!item.IsCardOpened)
        {
            item.OpenCard();
            if (item.CardPair == _lastOpenedCard.CardPair)
            {
                _isPlayerOpenedACard = false;
                _isCardOpenning = false;
                _pairsSolved++;
                if(_pairsSolved>=6)
                    OnGameFinished?.Invoke();
                yield break;
            }

            yield return new WaitForSeconds(openCardTime);

            item.CloseCard();
            _lastOpenedCard.CloseCard();
            _isPlayerOpenedACard=false;
            _lastOpenedCard = null;

        }
        _isCardOpenning = false;

    }
    private void ShufflePositions()
    {
        for (int i = 0; i < puzzleCardsList.Count - 1; i++)
        {
            var temp = puzzleCardsList[i].rectTransform.position;

            int rand = Random.Range(i, puzzleCardsList.Count);
            puzzleCardsList[i].rectTransform.position = puzzleCardsList[rand].rectTransform.position;
            puzzleCardsList[rand].rectTransform.position = temp;
        }
    }

}
