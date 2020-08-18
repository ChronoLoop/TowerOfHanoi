using UnityEngine;
using System.Runtime.InteropServices;
using TMPro;

public class TextOpenLink : MonoBehaviour
{
    /*reference for hyperlinks: 
    https://deltadreamgames.com/unity-tmp-hyperlinks
    https://www.youtube.com/watch?v=qqOqLNqAdDo
    */
    [SerializeField] private TextMeshProUGUI text;
    [DllImport("__Internal")]
    private static extern void openWindow(string url);
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);

            if (linkIndex != -1)
            {
                string hyperlink = text.textInfo.linkInfo[linkIndex].GetLinkID();
                if (Application.platform == RuntimePlatform.WebGLPlayer)
                {
                    openWindow(hyperlink);
                }
                else
                {
                    Application.OpenURL(hyperlink);
                }
            }
        }
    }
}
