using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PuzzleCluesManager : MonoBehaviour, IDataPersistance
{
    [Header("References")]
    [Space]
    [SerializeField] private List<PuzzleClueHolder> unusedCluesHoldersList = new List<PuzzleClueHolder>();
    [SerializeField] private List<PuzzleClueHolder> usedCluesHoldersList = new List<PuzzleClueHolder>();

    private PuzzleGamesEnvironmentsHolder _puzzleGamesEnvironmentsHolder;
    private DataPersistanceManager _dataPersistanceManager;

    private int environmentIndex;

    private PuzzleClueHolder currentlyActiveClue;

    #region Zenject
    [Inject]
    private void Construct(PuzzleGamesEnvironmentsHolder puzzleGamesEnvironmentsHolder, DataPersistanceManager dataPersistanceManager)
    {
        _puzzleGamesEnvironmentsHolder = puzzleGamesEnvironmentsHolder;
        _dataPersistanceManager = dataPersistanceManager;
    }
    #endregion Zenject

    private void Awake()
    {
        _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
    }

    public void SetEnvironmentIndex(int index)
    {
        environmentIndex = index;
    }

    public void ShowRandomClue()
    {
        if(currentlyActiveClue != null)
        {
            if(currentlyActiveClue.IsActive)
            {
                currentlyActiveClue.HideClue();
            }
        }

        if(unusedCluesHoldersList.Count > 0)
        {
            int randomIndex = Random.Range(0, unusedCluesHoldersList.Count);

            unusedCluesHoldersList[randomIndex].ShowClue();

            currentlyActiveClue = unusedCluesHoldersList[randomIndex];
        }
    }

    public void KeyCollected_ExecuteReaction(int index)
    {
        PuzzleClueHolder keyRelatedClueHolder = null;

        for(int i = 0; i < unusedCluesHoldersList.Count; i++)
        {
            if(unusedCluesHoldersList[i].ClueIndex == index)
            {
                keyRelatedClueHolder = unusedCluesHoldersList[i];
                break;
            }
        }

        unusedCluesHoldersList.Remove(keyRelatedClueHolder);
        usedCluesHoldersList.Add(keyRelatedClueHolder);
        keyRelatedClueHolder.HideClue();
    }

    public void LoadData(GameData data)
    {
        List<int> unavailableCluesList = new List<int>();

        unavailableCluesList = data.puzzleGameLevelsDataList[environmentIndex].unavailableCluesList;

        for (int i = 0; i < unavailableCluesList.Count; i++)
        {
            for (int j = 0; j < unusedCluesHoldersList.Count; j++)
            {
                if (unavailableCluesList[i] == unusedCluesHoldersList[j].ClueIndex)
                {
                    PuzzleClueHolder usedClue = unusedCluesHoldersList[j];
                    unusedCluesHoldersList.Remove(usedClue);
                    usedCluesHoldersList.Add(usedClue);
                    break;
                }
            }
        }
    }

    public void SaveData(GameData data)
    {
        List<int> usedCluesIndexes = new List<int>();

        for(int i = 0; i < usedCluesHoldersList.Count; i++)
        {
            usedCluesIndexes.Add(usedCluesHoldersList[i].ClueIndex);
        }

        if(usedCluesIndexes.Count > 0)
        data.puzzleGameLevelsDataList[environmentIndex].unavailableCluesList = usedCluesIndexes;
    }
}
