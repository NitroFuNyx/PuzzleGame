using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PuzzleLocksHolder : MonoBehaviour, IDataPersistance
{
    [Header("Locks")]
    [Space]
    [SerializeField] private List<PuzzleLock> allLocksList = new List<PuzzleLock>();
    [SerializeField] private List<int> openedLocksList = new List<int>();

    private DataPersistanceManager _dataPersistanceManager;
    private PuzzleGamesEnvironmentsHolder _puzzleGamesEnvironmentsHolder;

    private void Awake()
    {
        for(int i = 0; i < allLocksList.Count; i++)
        {
            allLocksList[i].CashComponents(this);
        }

        _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
    }

    #region Zenject
    [Inject]
    private void Construct(DataPersistanceManager dataPersistanceManager, PuzzleGamesEnvironmentsHolder puzzleGamesEnvironmentsHolder)
    {
        _dataPersistanceManager = dataPersistanceManager;
        _puzzleGamesEnvironmentsHolder = puzzleGamesEnvironmentsHolder;
    }
    #endregion Zenject

    public void LoadData(GameData data)
    {
        for (int i = 0; i < data.puzzleGameLevelsDataList[0].openedLocksList.Count; i++)
        {
            openedLocksList.Add(data.puzzleGameLevelsDataList[0].openedLocksList[i]);
        }

        for(int i = 0; i < allLocksList.Count; i++)
        {
            if(openedLocksList.Contains(allLocksList[i].LockIndex))
            {
                allLocksList[i].gameObject.SetActive(false);
            }
        }
    }

    public void SaveData(GameData data)
    {
        List<int> openedLocksIndexesList = new List<int>();
       
        for (int i = 0; i < openedLocksList.Count; i++)
        {
            openedLocksIndexesList.Add(openedLocksList[i]);
        }

        data.puzzleGameLevelsDataList[0].openedLocksList = openedLocksIndexesList;
    }

    public void AddLockToOpenList(PuzzleLock puzzleLock)
    {
        openedLocksList.Add(puzzleLock.LockIndex);
        _dataPersistanceManager.SaveGame();
    }
}
