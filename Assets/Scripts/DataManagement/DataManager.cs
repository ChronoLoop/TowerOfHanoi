using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//reference: https://www.youtube.com/watch?v=XOjd_qU2Ido&t=118s
public static class DataManager
{
    static string gameSettingPath;
    static DataManager()
    {
        gameSettingPath = Application.persistentDataPath + "/gamesetting.data";
    }
    public static void SaveGameSetting(GameSetting gameSetting)
    {
        BinaryFormatter binaryFormetter = new BinaryFormatter();
        FileStream fs = new FileStream(gameSettingPath, FileMode.Create);

        GameSettingData data = new GameSettingData(gameSetting);

        binaryFormetter.Serialize(fs, data);
        fs.Close();
    }

    public static GameSettingData LoadGameSetting()
    {
        return LoadData<GameSettingData>(gameSettingPath);
    }

    #region Helper Functions
    private static T LoadData<T>(string path) where T : class
    {
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);

            T data = binaryFormatter.Deserialize(fs) as T;
            fs.Close();

            return data;
        }
        else
        {
            LogSaveFileError(path);
            return null;
        }
    }

    private static void LogSaveFileError(string path)
    {
        Debug.LogError("Save file not found in " + path);
    }
    #endregion
}