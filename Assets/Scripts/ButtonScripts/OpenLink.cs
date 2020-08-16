using UnityEngine;

public class OpenLink : MonoBehaviour
{
    //reference for hyperlinks: https://www.youtube.com/watch?v=qqOqLNqAdDo
    public void OpenBecrisLink()
    {
        Application.OpenURL("https://www.flaticon.com/authors/Becris");
    }
    public void OpenChanutLink()
    {
        Application.OpenURL("https://www.flaticon.com/authors/Chanut");
    }
    public void OpenFlaticonLink()
    {
        Application.OpenURL("https://www.flaticon.com");
    }
    public void OpenZapsplatLink()
    {
        Application.OpenURL("https://www.zapsplat.com");
    }
}
