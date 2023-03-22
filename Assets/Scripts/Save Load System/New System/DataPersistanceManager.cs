using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DataPersistanceManager : MonoBehaviour
{
    private List<IDataPersistance> saveSystemDataObjectsList = new List<IDataPersistance>();

    private PlayerDataManager _playerDataManager;

    private GameData gameData;

    private void Start()
    {
        StartCoroutine(LoadStartDataCoroutine());
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
        gameData = new GameData(_playerDataManager); // save class

        FileDataHandler.Save(gameData); // create file with basic data

        InitializeMiniGameLevelsIndexes(gameData);

        SaveGame(); // save actual data set in Unity
    }

    public void SaveGame()
    {
        for (int i = 0; i < saveSystemDataObjectsList.Count; i++)
        {
            saveSystemDataObjectsList[i].SaveData(gameData);
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
        }

        for (int i = 0; i < saveSystemDataObjectsList.Count; i++)
        {
            saveSystemDataObjectsList[i].LoadData(gameData);
        }
    }

    public void AddObjectToSaveSystemObjectsList(IDataPersistance saveSystemObject)
    {
        saveSystemDataObjectsList.Add(saveSystemObject);
    }

    private void InitializeMiniGameLevelsIndexes(GameData gameData)
    {
        for (int i = 0; i < gameData.miniGameLevelsDataList.Length; i++)
        {
            gameData.miniGameLevelsDataList[i].levelIndex = i;
        }
    }

    private IEnumerator LoadStartDataCoroutine()
    {
        yield return null;
        LoadGame();
    }
}
