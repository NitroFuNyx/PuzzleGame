using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DataPersistanceManager : MonoBehaviour
{
    [Header("Save System Data Objects")]
    [Space]
    [SerializeField] private List<IDataPersistance> saveSystemDataObjectsList = new List<IDataPersistance>();

    private PlayerDataManager _playerDataManager;

    private GameData gameData;

    private void Start()
    {
        LoadGame();
    }

    #region Zenject
    [Inject]
    private void Construct(PlayerDataManager playerDataManager)
    {
        _playerDataManager = playerDataManager;
    }
    #endregion Zenject

    public void NewGame()
    {
        gameData = new GameData();

        gameData.currentCoinsAmount = 0;
        gameData.languageIndex = 0;
        gameData.soundMuted = false;
        gameData.miniGameLevelCoins = 0;
    }

    public void SaveGame()
    {
        for (int i = 0; i < saveSystemDataObjectsList.Count; i++)
        {
            saveSystemDataObjectsList[i].SaveData(gameData);
            Debug.Log($"Game mute {gameData.soundMuted}");
        }

        FileDataHandler.Save(gameData);
    }

    public void LoadGame()
    {
        gameData = FileDataHandler.Load();

        if(gameData == null)
        {
            Debug.Log($"No Save Files Found.");
            Debug.Log($"Creating New Save File.");
            NewGame();
            StartCoroutine(CreateNewSaveCoroutine());
        }
        else
        {
            for (int i = 0; i < saveSystemDataObjectsList.Count; i++)
            {
                saveSystemDataObjectsList[i].LoadData(gameData);
            }
        }
    }

    public void AddObjectToSaveSystemObjectsList(IDataPersistance saveSystemObject)
    {
        saveSystemDataObjectsList.Add(saveSystemObject);
    }

    private IEnumerator CreateNewSaveCoroutine()
    {
        yield return null;
        SaveGame();

        yield return null;
        for (int i = 0; i < saveSystemDataObjectsList.Count; i++)
        {
            saveSystemDataObjectsList[i].LoadData(gameData);
        }
    }
}
