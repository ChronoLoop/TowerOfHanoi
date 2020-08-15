[System.Serializable]
public class SoundSettingData
{
    public float gameVolume;
    public float musicVolume;
    public SoundSettingData(SoundManager soundManager)
    {
        gameVolume = soundManager.gameVolume;
        musicVolume = soundManager.musicVolume;
    }
}