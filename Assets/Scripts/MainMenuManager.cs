using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject creditsGameObj;
    [SerializeField] private GameObject mainMenuGameObj;
    [SerializeField] private GameObject settingsGameObj;
    private void Awake()
    {
        creditsGameObj.SetActive(false);
        settingsGameObj.SetActive(false);
        mainMenuGameObj.SetActive(true);
    }
    public void EnterCredits()
    {
        mainMenuGameObj.SetActive(false);
        creditsGameObj.SetActive(true);
    }
    public void ExitCredits()
    {
        mainMenuGameObj.SetActive(true);
        creditsGameObj.SetActive(false);
    }
    public void EnterSettings()
    {
        settingsGameObj.SetActive(true);
        mainMenuGameObj.SetActive(false);
    }
    public void ExitSettings()
    {
        settingsGameObj.SetActive(false);
        mainMenuGameObj.SetActive(true);
    }
    public void PlayButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}