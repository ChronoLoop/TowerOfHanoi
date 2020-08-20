using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 ZoomFactor;
    private Vector3 CameraPosition;
    private Vector2 screenResolution;
    private float targetResolutionRatio;
    private void Awake()
    {
        screenResolution = new Vector2(Screen.width, Screen.height);
        targetResolutionRatio = 16f / 9f;
        ZoomFactor = new Vector3(0f, -11f, 18f);
        CameraPosition = transform.position;
        AdjustCameraPosition();
    }
    /// <summary>
    /// when the resolution changes
    /// reposition the camera so that the board is always at the bottom of the screen
    /// </summary>
    private void Update()
    {
        if (HasResolutionChanged())
        {
            AdjustCameraPosition();
        }
    }
    private bool HasResolutionChanged()
    {
        //reference: https://answers.unity.com/questions/969731/how-do-i-check-if-the-screengamewindow-has-changed.html
        if (screenResolution.x != Screen.width || screenResolution.y != Screen.height)
        {
            //reset resolution variable
            screenResolution.x = Screen.width;
            screenResolution.y = Screen.height;
            return true;
        }
        return false;
    }
    private void AdjustCameraPosition()
    {
        if (screenResolution.y == 0 || screenResolution.x == 0)
            return;
        //reference for maintaining aspect ratio: https://www.youtube.com/watch?v=So-UJYoOPr8
        //reposition camera based on zoom, screen and target ratios
        float screenRatio = (float)screenResolution.x / (float)screenResolution.y;
        float ratioSizeDiff = targetResolutionRatio / screenRatio;
        transform.position = CameraPosition
                                            + transform.forward * (1f - ratioSizeDiff) * ZoomFactor.z
                                            + transform.right * (1f - ratioSizeDiff) * ZoomFactor.x
                                            + transform.up * (1f - ratioSizeDiff) * ZoomFactor.y;
    }
}