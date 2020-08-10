using UnityEngine;
using UnityEngine.UI;

public class GameSetting : MonoBehaviour
{
    public float gameVolume { get; set; }
    public float musicVolume { get; set; }
    public Slider gameVolSlider, musicVolSlider;
    private bool valuesLoaded = false;
    private void Awake()
    {
        LoadGameSetting();
        gameVolSlider.value = gameVolume;
        musicVolSlider.value = musicVolume;
        valuesLoaded = true;
    }

    /*
    issue: chaning values of sliders in awake causes the updatesound to be called 
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
            SaveGameSetting();
        }
    }

    private void SaveGameSetting()
    {
        DataManager.SaveGameSetting(this);
    }
    private void LoadGameSetting()
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