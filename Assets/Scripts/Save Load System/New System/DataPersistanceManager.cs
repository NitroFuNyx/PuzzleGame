using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistanceManager : MonoBehaviour
{
    private GameData gameData;

    public void NewGame()
    {
        gameData = new GameData();
    }

    public void SaveGame()
    {

    }

    public void LoadGame()
    {
        if(gameData == null)
        {
            Debug.Log($"No Save Files Found.");
            Debug.Log($"Creating New Save File.");
            NewGame();
        }
    }
}
