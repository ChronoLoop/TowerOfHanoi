using UnityEngine;

public class DiskController : MonoBehaviour
{
    public int size;
    public Color color;
    public Rigidbody rb;
    //mouse data
    private Vector3 mouseOffset;
    private float mouseZCoord;

    private void Start()
    {
        rb.freezeRotation = true;
    }

    #region Drag and Drop functions
    private void OnMouseDown()
    {
        if (CanDragDisk())
        {
            mouseZCoord = Camera.main.WorldToScreenPoint(transform.position).z;
            mouseOffset = transform.position - GetMouseWorldPos();
        }
    }
    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mouseOffset;
    }
    #endregion

    #region Helper Functions
    //reference for drag and drop: https://www.youtube.com/watch?v=0yHBDZHLRbQ
    private bool CanDragDisk()
    {
        //TODO: check if this disk is the topmost disk
        return true;
    }

    private Vector3 GetMouseWorldPos()
    {
        //pixel coordinates (x,y)
        Vector3 mousePoint = Input.mousePosition;
        //z coordinate of game
        mousePoint.z = mouseZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    #endregion
}