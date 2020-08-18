using UnityEngine;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

//reference: https://www.youtube.com/watch?v=XOjd_qU2Ido&t=118s
public static class DataManager
{
    //import exteral method from plugin/webgl/HandlIO.jslib
    [DllImport("__Internal")]
    private static extern void SyncFiles();
    static string soundSettingPath;
    static string levelDataPath;
    static DataManager()
    {
        soundSettingPath = Application.persistentDataPath + "/soundsettings.data";
        levelDataPath = Application.persistentDataPath + "/levels.data";
    }
    #region soundsetting
    public static void SaveSoundSetting(SoundManager soundManager)
    {
        try
        {
            BinaryFormatter binaryFormetter = new BinaryFormatter();
            FileStream fs = new FileStream(soundSettingPath, FileMode.Create);

            SoundSettingData data = new SoundSettingData(soundManager);
            binaryFormetter.Serialize(fs, data);
            fs.Close();
            WebGLSyncFile();
        }
        catch
        {
            LogSaveFileError(soundSettingPath);
        }
    }
    public static SoundSettingData LoadSoundSetting()
    {
        return LoadData<SoundSettingData>(soundSettingPath);
    }
    #endregion

    #region leveldata
    public static void SaveLevelData(GameManager gameManager)
    {
        try
        {
            BinaryFormatter binaryFormetter = new BinaryFormatter();
            FileStream fs = new FileStream(levelDataPath, FileMode.Create);

            LevelData data = new LevelData(gameManager);

            binaryFormetter.Serialize(fs, data);
            fs.Close();
            WebGLSyncFile();
        }
        catch
        {
            LogSaveFileError(soundSettingPath);
        }
    }
    public static LevelData LoadLevelData()
    {
        return LoadData<LevelData>(levelDataPath);
    }
    #endregion

    #region Helper Functions
    private static T LoadData<T>(string path) where T : class
    {
        if (File.Exists(path))
        {
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream fs = new FileStream(path, FileMode.Open);

                T data = binaryFormatter.Deserialize(fs) as T;
                fs.Close();

                return data;
            }
            catch
            {
                LogLoadFileError(path);
                return null;
            }
        }
        else
        {
            return null;
        }
    }
    private static void LogLoadFileError(string path)
    {
        Debug.LogError("Failed to load file: " + path);
    }
    private static void LogSaveFileError(string path)
    {
        Debug.LogError("Failed to save file: " + path);
    }
    private static void WebGLSyncFile()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            SyncFiles();
        }
    }
    #endregion
}