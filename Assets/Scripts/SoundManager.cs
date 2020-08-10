using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;
    public float gameVolume { get; private set; }
    public float musicVolume { get; private set; }
    public Slider gameVolSlider, musicVolSlider;
    private bool valuesLoaded = false;

    private void Awake()
    {
        LoadSettingSounds();
        gameVolSlider.value = gameVolume;
        musicVolSlider.value = musicVolume;
        valuesLoaded = true;

        backgroundMusic.loop = true;
        backgroundMusic.volume = musicVolume;
        backgroundMusic.Play();
    }
    /*
    issue: changing values of sliders in awake causes the updatesound to be called 
    which could set the member volume variables to default values on the sliders.
    fix: use a bool to check if all values are loaded.
    another possible fix: is to update game and music volume in separate functions.
    */
    public void UpdateSound()
    {
        if (valuesLoaded)
        {
            gameVolume = gameVolSlider.value;
            musicVolume = musicVolSlider.value;
            SetMusicVolume();
            SaveSoundSetting();
        }
    }

    private void SetMusicVolume()
    {
        backgroundMusic.volume = musicVolume;
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
}