using System.Collections.Generic;
using UnityEngine;

public class MiniGamesEnvironmentsHolder : MonoBehaviour
{
    [Header("Mini Games Levels")]
    [Space]
    [SerializeField] private List<MiniGameEnvironment> gamesEnvironmentsList = new List<MiniGameEnvironment>();

    private MiniGameEnvironment currentlyActiveGame;

    public MiniGameEnvironment CurrentlyActiveGame { get => currentlyActiveGame; private set => currentlyActiveGame = value; }

    public void ActivateEnvironment(int levelIndex)
    {
        HideAllEnvironments();

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

    private void HideAllEnvironments()
    {
        for (int i = 0; i < gamesEnvironmentsList.Count; i++)
        {
            gamesEnvironmentsList[i].gameObject.SetActive(false);
        }
    }
}
