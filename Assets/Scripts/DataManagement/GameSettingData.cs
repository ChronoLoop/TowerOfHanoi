using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSettingData
{
    public float gameVolume;
    public float musicVolume;
    public GameSettingData(GameSetting gameSetting)
    {
        gameVolume = gameSetting.gameVolume;
        musicVolume = gameSetting.musicVolume;
    }
}