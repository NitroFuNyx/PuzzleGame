using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class FileDataHandler
{
    //private const string PlayerDataSaveFilePath = "Assets/Save Files/PlayerSaveFile.json";
    private const string PlayerDataSaveFilePath = "Assets/Save Files/PlayerMainDataSave.bsf";
    //private static string playerDataSaveFilePathInBuild = $"{Application.persistentDataPath}/{fileName}";
    private static string playerDataSaveFilePathInBuild = $"{Application.persistentDataPath}/PlayerMainDataSave.bsf";

    //private static string fileName = "PlayerSaveFile.json";

    public static GameData Read()
    {
        GameData data = null;

        if (File.Exists(GetSaveFilePath()))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(GetSaveFilePath(), FileMode.Open);

            data = formatter.Deserialize(stream) as GameData;
            stream.Close();
        }
        else
        {
            Debug.Log($"No Save File {GetSaveFilePath()}");
        }

        return data;


        //GameData loadedData = null;

        //if (File.Exists(GetSaveFilePath()))
        //{
        //    string dataToLoad = "";

        //    FileStream stream = new FileStream(GetSaveFilePath(), FileMode.Open);

        //    StreamReader reader = new StreamReader(stream);

        //    dataToLoad = reader.ReadToEnd();

        //    loadedData = JsonUtility.FromJson<GameData>(dataToLoad);

        //    stream.Close();
        //}
        //else
        //{
        //    Debug.Log($"No Save File {GetSaveFilePath()}");
        //}

        //return loadedData;
    }

    public static void Write(GameData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(GetSaveFilePath(), FileMode.Create);

        //PlayerMainData playerMainData = new PlayerMainData(playerDataManager);

        formatter.Serialize(stream, data);
        stream.Close();


        //string dataToStore = JsonUtility.ToJson(data, true);

        //using (FileStream stream = new FileStream(GetSaveFilePath(), FileMode.Create))
        //{
        //    using (StreamWriter writer = new StreamWriter(stream))
        //    {
        //        writer.Write(dataToStore);
        //    }
        //    stream.Close();
        //}
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
