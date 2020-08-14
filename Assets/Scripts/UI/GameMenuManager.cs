using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject settingMenuUI;

    private void Awake()
    {
        pauseMenuUI.SetActive(false);
        settingMenuUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseButtonClick();
        }
    }
    #region Icon Buttons on click functions
    public void PauseButtonClick()
    {
        if (!GameManager.gameIsPaused)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
        }
        GameManager.gameIsPaused = !GameManager.gameIsPaused;
    }
    public void SettingButtonClick()
    {
        settingMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }
    public void ExitSettingButtonClick()
    {
        settingMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
    public void QuitButtonClick()
    {
        GameManager.gameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    #endregion
}