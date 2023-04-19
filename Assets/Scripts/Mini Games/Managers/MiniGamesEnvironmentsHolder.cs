using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MiniGamesEnvironmentsHolder : MonoBehaviour
{
    [Header("Mini Games Levels")]
    [Space]
    [SerializeField] private List<MiniGameEnvironment> gamesEnvironmentsList = new List<MiniGameEnvironment>();

    private CurrentGameManager _currentGameManager;

    private MiniGameEnvironment currentlyActiveGame;

    public MiniGameEnvironment CurrentlyActiveGame { get => currentlyActiveGame; private set => currentlyActiveGame = value; }

    private void Start()
    {
        HideAllEnvironments();
    }

    #region Zenject
    [Inject]
    private void Construct(CurrentGameManager currentGameManager)
    {
        _currentGameManager = currentGameManager;
    }
    #endregion Zenject

    public void ActivateEnvironment(int levelIndex)
    {
        HideAllEnvironments();
        _currentGameManager.HidePuzzleEnvironment();
        
        for(int i = 0; i < gamesEnvironmentsList.Count; i++)
        {
            if(gamesEnvironmentsList[i].EnvironmentIndex == levelIndex)
            {
                gamesEnvironmentsList[i].gameObject.SetActive(true);
                currentlyActiveGame = gamesEnvironmentsList[i];
                currentlyActiveGame.StartGame();
                break;
            }
        }
    }

    public void HideAllEnvironments()
    {
        for (int i = 0; i < gamesEnvironmentsList.Count; i++)
        {
            gamesEnvironmentsList[i].gameObject.SetActive(false);
        }

        Debug.Log($"Hide Mini Game: Game active {gamesEnvironmentsList[0].gameObject.activeInHierarchy}");
    }
}
