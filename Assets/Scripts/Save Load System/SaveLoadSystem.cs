using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoadSystem
{
    private const string PlayerDataSaveFilePath = "Assets/Save Files/PlayerMainDataSave.bsf";
    private static string playerDataSaveFilePathInBuild = $"{Application.persistentDataPath}/PlayerMainDataSave.bsf";

    public static void SavePlayerData(PlayerDataManager playerDataManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
 
        FileStream stream = new FileStream(GetSaveFilePath(), FileMode.Create);

        PlayerMainData playerMainData = new PlayerMainData(playerDataManager);

        formatter.Serialize(stream, playerMainData);
        stream.Close();
    }

    public static PlayerMainData LoadPlayerData()
    {
        PlayerMainData data = null;

        if (File.Exists(GetSaveFilePath()))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(GetSaveFilePath(), FileMode.Open);

            data = formatter.Deserialize(stream) as PlayerMainData;
            stream.Close();
        }
        else
        {
            Debug.Log($"No Save File {GetSaveFilePath()}");
        }

         return data;
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
