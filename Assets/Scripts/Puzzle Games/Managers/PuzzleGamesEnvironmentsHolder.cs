using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGamesEnvironmentsHolder : MonoBehaviour
{
    [Header("Puzzle Games Levels")]
    [Space]
    [SerializeField] private List<PuzzleGameEnvironment> gamesEnvironmentsList = new List<PuzzleGameEnvironment>();

    private PuzzleGameEnvironment currentlyActiveGame;

    public PuzzleGameEnvironment CurrentlyActiveGame { get => currentlyActiveGame; private set => currentlyActiveGame = value; }

    private void Start()
    {
        StartCoroutine(HideLevelsCoroutine());
    }

    public void ActivateEnvironment(int levelIndex)
    {
        HideAllEnvironments();

        for (int i = 0; i < gamesEnvironmentsList.Count; i++)
        {
            if (gamesEnvironmentsList[i].EnvironmentIndex == levelIndex)
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

    private IEnumerator HideLevelsCoroutine()
    {
        yield return new WaitForSeconds(2f);
        HideLevelsCoroutine();
        HideAllEnvironments();
    }
}
