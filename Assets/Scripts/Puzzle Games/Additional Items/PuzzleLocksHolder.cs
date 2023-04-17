using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class PuzzleLocksHolder : MonoBehaviour, IDataPersistance
{
    [Header("Locks")]
    [Space]
    [SerializeField] private List<PuzzleLock> allLocksList = new List<PuzzleLock>();
    [SerializeField] private List<int> openedLocksList = new List<int>();
    [Header("Change Alpha Data")]
    [Space]
    [SerializeField] private float changeAlphaDuration;

    private DataPersistanceManager _dataPersistanceManager;
    private PuzzleGamesEnvironmentsHolder _puzzleGamesEnvironmentsHolder;
    private PuzzleGameUI _puzzleGameUI;
    private AudioManager _audioManager;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        for(int i = 0; i < allLocksList.Count; i++)
        {
            allLocksList[i].CashComponents(this);
        }

        _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    #region Zenject
    [Inject]
    private void Construct(DataPersistanceManager dataPersistanceManager, PuzzleGamesEnvironmentsHolder puzzleGamesEnvironmentsHolder, PuzzleGameUI puzzleGameUI,
                           AudioManager audioManager)
    {
        _dataPersistanceManager = dataPersistanceManager;
        _puzzleGamesEnvironmentsHolder = puzzleGamesEnvironmentsHolder;
        _puzzleGameUI = puzzleGameUI;
        _audioManager = audioManager;
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
       
        if(!_puzzleGamesEnvironmentsHolder.GamesEnvironmentsList[0].GameFinished)
        {
            for (int i = 0; i < openedLocksList.Count; i++)
            {
                openedLocksIndexesList.Add(openedLocksList[i]);
            }
        }
        else
        {
            openedLocksList.Clear();
        }

        data.puzzleGameLevelsDataList[0].openedLocksList = openedLocksIndexesList;
    }

    public void AddLockToOpenList(PuzzleLock puzzleLock)
    {
        openedLocksList.Add(puzzleLock.LockIndex);
        _dataPersistanceManager.SaveGame();
        CheckClodesLocksAmount();
    }

    public void LockSelect(PuzzleLock puzzleLock)
    {
        if (_puzzleGameUI.InventoryPanel.CurrentlySelectedInventoryCell != null)
        {
            if ((int)_puzzleGameUI.InventoryPanel.CurrentlySelectedInventoryCell.ItemType == puzzleLock.LockIndex)
            {
                _audioManager.PlaySFXSound_OpenLock();
                puzzleLock.OpenLock();
                _puzzleGameUI.InventoryPanel.ItemUsed_ExecuteReaction();
            }
            else
            {
                puzzleLock.ResetLock();
                _puzzleGameUI.InventoryPanel.LockKeyMismatched_ExecuteReaction();
            }
        }
    }

    public void ResetLocks()
    {
        for(int i = 0; i < allLocksList.Count; i++)
        {
            allLocksList[i].gameObject.SetActive(true);
            allLocksList[i].ResetLock();
        }

        spriteRenderer.DOFade(1f, changeAlphaDuration);
    }

    private void CheckClodesLocksAmount()
    {
        if(openedLocksList.Count == allLocksList.Count)
        {
            _puzzleGamesEnvironmentsHolder.CurrentlyActiveGame.AllLocksOpened_ExecuteReaction();
            spriteRenderer.DOFade(0f, changeAlphaDuration);
        }
    }
}
