using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadCreditsScene()
    {
        SceneManager.LoadScene(2);
    }
}