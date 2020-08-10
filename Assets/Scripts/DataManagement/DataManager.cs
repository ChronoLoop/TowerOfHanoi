using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//reference: https://www.youtube.com/watch?v=XOjd_qU2Ido&t=118s
public static class DataManager
{
    static string soundSettingPath;
    static DataManager()
    {
        soundSettingPath = Application.persistentDataPath + "/soundsettings.data";
    }
    public static void SaveSoundSetting(SoundManager soundManager)
    {
        BinaryFormatter binaryFormetter = new BinaryFormatter();
        FileStream fs = new FileStream(soundSettingPath, FileMode.Create);

        SoundSettingData data = new SoundSettingData(soundManager);

        binaryFormetter.Serialize(fs, data);
        fs.Close();
    }

    public static SoundSettingData LoadSoundSetting()
    {
        return LoadData<SoundSettingData>(soundSettingPath);
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