using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject creditsGameObj;
    [SerializeField] private GameObject mainMenuGameObj;
    private void Awake()
    {
        creditsGameObj.SetActive(false);
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
    public void PlayButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}