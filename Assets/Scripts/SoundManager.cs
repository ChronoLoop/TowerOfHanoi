using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource diskDropSoundEffect;
    public float gameVolume { get; private set; }
    public float musicVolume { get; private set; }
    public Slider gameVolSlider, musicVolSlider;

    private void Awake()
    {
        LoadSettingSounds();
        gameVolSlider.value = gameVolume;
        musicVolSlider.value = musicVolume;

        backgroundMusic.loop = true;
        backgroundMusic.volume = musicVolume;
        backgroundMusic.Play();
    }
    public void UpdateGameVolume()
    {
        gameVolume = gameVolSlider.value;
        SetGameVolume();
        SaveSoundSetting();
    }
    public void UpdateMusicVolume()
    {
        musicVolume = musicVolSlider.value;
        SetMusicVolume();
        SaveSoundSetting();
    }
    private void SetMusicVolume()
    {
        backgroundMusic.volume = musicVolume;
    }
    private void SetGameVolume()
    {
        diskDropSoundEffect.volume = gameVolume;
    }

    private void SaveSoundSetting()
    {
        DataManager.SaveSoundSetting(this);
    }
    private void LoadSettingSounds()
    {
        SoundSettingData data = DataManager.LoadSoundSetting();
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
    public void PlayDiskDropSoundEffect()
    {
        diskDropSoundEffect.Play();
    }
}