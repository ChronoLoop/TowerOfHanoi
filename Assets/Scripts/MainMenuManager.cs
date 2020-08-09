using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject creditsGameObj;
    private void Awake()
    {
        creditsGameObj.SetActive(false);
    }
    public void EnterCredits()
    {
        creditsGameObj.SetActive(true);
    }
    public void ExitCredits()
    {
        creditsGameObj.SetActive(false);
    }
    
}