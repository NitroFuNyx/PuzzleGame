using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DataPersistanceManager : MonoBehaviour
{
    private float loadStartDataDelay = 0.1f;

    private List<IDataPersistance> saveSystemDataObjectsList = new List<IDataPersistance>();

    private GameDataHolder _gameDataHolder;

    private GameData gameData;

    private void Start()
    {
        StartCoroutine(LoadStartDataCoroutine());
    }

    #region Zenject
    [Inject]
    private void Construct(GameDataHolder gameDataHolder)
    {
        _gameDataHolder = gameDataHolder;
    }
    #endregion Zenject

    public void NewGame()
    {
        gameData = new GameData(_gameDataHolder); // make class with default data

        FileDataHandler.Write(gameData); // create json file and write default data

        InitializeMiniGameLevelsData(gameData);
        InitializePuzzleGameLevelsData(gameData);
        InitializeDailyRewardsData(gameData);

        SaveGame(); // save actual Unity data set in json file
    }

    public void SaveGame()
    {
        for (int i = 0; i < saveSystemDataObjectsList.Count; i++)
        {
            saveSystemDataObjectsList[i].SaveData(gameData);
        }

        FileDataHandler.Write(gameData);
    }

    public void LoadGame()
    {
        gameData = FileDataHandler.Read();

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

    private void InitializeMiniGameLevelsData(GameData gameData)
    {
        for (int i = 0; i < _gameDataHolder.MiniGameLevelsPanelsList.Count; i++)
        {
            MiniGameLevelData miniGameData = new MiniGameLevelData();
            gameData.miniGameLevelsDataList.Add(miniGameData);
            gameData.miniGameLevelsDataList[i].levelIndex = i;
            gameData.miniGameLevelsDataList[i].levelPrice = _gameDataHolder.MiniGameLevelsPanelsList[i].LevelPrice;
        }
    }

    private void InitializePuzzleGameLevelsData(GameData gameData)
    {
        for (int i = 0; i < _gameDataHolder.PuzzleGameLevelsPanelsList.Count; i++)
        {
            PuzzleGameLevelData puzzleGameData = new PuzzleGameLevelData();
            gameData.puzzleGameLevelsDataList.Add(puzzleGameData);
            gameData.puzzleGameLevelsDataList[i].levelIndex = i;
            gameData.puzzleGameLevelsDataList[i].levelPrice = _gameDataHolder.PuzzleGameLevelsPanelsList[i].LevelPrice;
        }
    }

    private void InitializeDailyRewardsData(GameData gameData)
    {
        gameData.lastDayPlaying = DateConstants.newGameIndexForData;
    }

    private IEnumerator LoadStartDataCoroutine()
    {
        yield return new WaitForSeconds(loadStartDataDelay);
        LoadGame();
    }
}
