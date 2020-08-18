using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public Text TextField = null;
    private int width = 0;
    private int height = 0;

    public void SetWidth(int width)
    {
        this.width = width;
    }
    public void SetHeight(int height)
    {
        this.height = height;
    }
    public void OnResize()
    {
        TextField.text = width.ToString() + "x" + height.ToString();
        Screen.SetResolution(width, height, false);
    }
    private void Update()
    {

    }
}