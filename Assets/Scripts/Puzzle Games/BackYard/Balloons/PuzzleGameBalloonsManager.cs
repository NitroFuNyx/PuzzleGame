using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PuzzleGameBalloonsManager : MonoBehaviour
{
    [SerializeField] private List<BalloonItem> balloonsItemsList;
    [SerializeField] private OrderScrambler balloonDestroyOrder;

    private int _currentColor;
    private int _colorsDestroyed;

    public event Action OnPuzzleRestart;
    public event Action OnPuzzleFinish;



    private void Start()
    {
        ShufflePositions();

        foreach (var balloon in balloonsItemsList)
        {
            balloon.OnButtonClick += ScoreBalloon;
        }
    }

    private void OnDestroy()
    {
        foreach (var balloon in balloonsItemsList)
        {
            balloon.OnButtonClick -= ScoreBalloon;
        }
    }

    private void RestartPuzzle()
    {
        _colorsDestroyed = 0;
        _currentColor = 0;

        OnPuzzleRestart?.Invoke();
    }

    private void ScoreBalloon(BalloonItem item)
    {
        for (int i = 0; i < balloonsItemsList.Count; i++)
        {
            if (item == balloonsItemsList[i])
            {
                if (balloonsItemsList[i].ItemColor == balloonDestroyOrder.ColorsEnumList[_currentColor])
                {
                    item.SwitchBalloonActivation(false);
                    _colorsDestroyed++;
                    if (_colorsDestroyed >= 3)
                    {
                        _currentColor++;
                        _colorsDestroyed = 0;
                    }

                    if (_currentColor >= 3 && _colorsDestroyed <= 0)
                    {
                        OnPuzzleFinish?.Invoke();
                    }
                    break;
                }
                else
                {
                    RestartPuzzle();
                }
            }
        }
    }
    
    private void ShufflePositions()
    {
        for (int i = 0; i < balloonsItemsList.Count - 1; i++)
        {
            var temp = balloonsItemsList[i].rectTransform.position;

            int rand = Random.Range(i, balloonsItemsList.Count);
            balloonsItemsList[i].rectTransform.position = balloonsItemsList[rand].rectTransform.position;
            balloonsItemsList[rand].rectTransform.position = temp;
        }
    }
}