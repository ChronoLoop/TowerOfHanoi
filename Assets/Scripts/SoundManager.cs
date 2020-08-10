using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;
    private float gameVolume;
    private float musicVolume;

    private void Awake()
    {
        backgroundMusic.loop = true;
        LoadSettingSounds();
        backgroundMusic.volume = musicVolume;
        backgroundMusic.Play();
    }

    private void LoadSettingSounds()
    {
        GameSettingData data = DataManager.LoadGameSetting();
        if (data == null)
        {
            gameVolume = 0.25f;
            musicVolume = 0.25f;
        }
        else
        {
            gameVolume = data.gameVolume;
            musicVolume = data.musicVolume;
        }
    }

}