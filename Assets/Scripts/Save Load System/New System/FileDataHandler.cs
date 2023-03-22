using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class FileDataHandler
{
    private const string PlayerDataSaveFilePath = "Assets/Save Files/Player Save Files/PlayerSaveFile.json";
    private static string playerDataSaveFilePathInBuild = $"{Application.persistentDataPath}/{fileName}";

    private static string fileName = "PlayerSaveFile.json";

    public static GameData Load()
    {
        GameData loadedData = null;

        if (File.Exists(GetSaveFilePath()))
        {
            string dataToLoad = "";

            FileStream stream = new FileStream(GetSaveFilePath(), FileMode.Open);

            StreamReader reader = new StreamReader(stream);

            dataToLoad = reader.ReadToEnd();

            loadedData = JsonUtility.FromJson<GameData>(dataToLoad);

            stream.Close();
        }
        else
        {
            Debug.Log($"No Save File {GetSaveFilePath()}");
        }

        return loadedData;
    }

    public static void Save(GameData data)
    {
        string dataToStore = JsonUtility.ToJson(data, true);

        using (FileStream stream = new FileStream(GetSaveFilePath(), FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(dataToStore);
            }
            stream.Close();
        }
    }

    private static string GetSaveFilePath()
    {
        string path = playerDataSaveFilePathInBuild;

#if UNITY_EDITOR
        path = PlayerDataSaveFilePath;
#endif

        return path;
    }
}