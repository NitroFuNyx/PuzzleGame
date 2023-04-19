using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PuzzleGamesEnvironmentsHolder : MonoBehaviour
{
    [Header("Puzzle Games Levels")]
    [Space]
    [SerializeField] private List<PuzzleGameEnvironment> gamesEnvironmentsList = new List<PuzzleGameEnvironment>();

    private PuzzleGameEnvironment currentlyActiveGame;
    private CurrentGameManager _currentGameManager;

    public PuzzleGameEnvironment CurrentlyActiveGame { get => currentlyActiveGame; private set => currentlyActiveGame = value; }
    public List<PuzzleGameEnvironment> GamesEnvironmentsList { get => gamesEnvironmentsList; }

    private void Start()
    {
        StartCoroutine(HideLevelsCoroutine());
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
        _currentGameManager.HideMiniGameEnvironment();

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

    public void HideAllEnvironments()
    {
        for (int i = 0; i < gamesEnvironmentsList.Count; i++)
        {
            gamesEnvironmentsList[i].gameObject.SetActive(false);
        }
        Debug.Log($"Hide Puzzle: Game active {gamesEnvironmentsList[0].gameObject.activeInHierarchy}");
    }

    private IEnumerator HideLevelsCoroutine()
    {
        yield return new WaitForSeconds(2f);
        HideAllEnvironments();
    }
}
