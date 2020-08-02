using UnityEngine;

public class DiskController : MonoBehaviour
{
    public int size;
    public Color color;
    public Rigidbody rb;
    //currentRod will be set by RodController
    public RodController currentRod;
    //mouse data
    private Vector3 mouseOffset;
    private float mouseZCoord;
    private Vector3 diskPositionBeforeMouseClick;
    private bool isOnRod;
    private bool isBeingDragged;
    private void Start()
    {
        //freeze z position
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        //freeze all rotations
        rb.freezeRotation = true;
        isOnRod = false;
        isBeingDragged = false;
    }

    private void FixedUpdate()
    {
        //Issue: disks would bounce even if disks have physics material with 0 bounciness
        //Fixed: if velocity is positive (meaning disks are bouncing/moving upward), set velocity to zero
        //refernce: https://answers.unity.com/questions/217639/physics-objects-passing-through-each-otherbouncing.html
        Vector3 currentVelocity = rb.velocity;
        if (currentVelocity.y <= 0f)
            return;

        currentVelocity.y = 0f;
        rb.velocity = currentVelocity;
    }

    #region Trigger
    private void OnTriggerEnter(Collider other)
    {
        if (isRod(other.gameObject))
        {
            isOnRod = true;
        }
        else
        {
            isOnRod = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //disk is not on a rod
        if (currentRod == null)
        {
            isOnRod = false;
        }
        //check if disk is exiting its current rod
        else if (currentRod == other.gameObject.GetComponent<RodController>())
        {
            isOnRod = false;
        }

    }
    #endregion


    #region Drag and Drop functions
    //reference for drag and drop: https://www.youtube.com/watch?v=0yHBDZHLRbQ
    private void OnMouseDown()
    {
        if (CanDragDisk())
        {
            mouseZCoord = Camera.main.WorldToScreenPoint(transform.position).z;
            mouseOffset = transform.position - GetMouseWorldPos();
            diskPositionBeforeMouseClick = transform.position;
            isBeingDragged = true;
        }
    }
    private void OnMouseDrag()
    {
        if (isBeingDragged)
        {
            //z axis is locked
            transform.position = new Vector3(
                GetMouseWorldPos().x + mouseOffset.x,
                GetMouseWorldPos().y + mouseOffset.y,
                transform.position.z
            );
        }
    }
    private void OnMouseUp()
    {
        //if disk is not on Rod, reset the disks position to diskPositionBeforeMouseClick
        if (isOnRod)
        {
            //Debug.Log("MouseUp: is on rod");
        }
        else if (isBeingDragged)
        {
            isOnRod = false;
            transform.position = diskPositionBeforeMouseClick;
            //Debug.Log("resetting position of disk");
        }

        isBeingDragged = false;
    }
    #endregion

    #region Helper Functions
    private bool CanDragDisk()
    {
        //TODO: check if this disk is the topmost disk
        if (isOnRod && (currentRod.GetTopDisk().size == this.size))
        {
            //Debug.Log("top disk");
            return true;
        }
        else
        {
            //Debug.Log("not top disk");
            return false;
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        //pixel coordinates (x,y)
        Vector3 mousePoint = Input.mousePosition;
        //z coordinate of game
        mousePoint.z = mouseZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    private bool isRod(GameObject obj)
    {
        //if obj is rod return true else return false
        return obj.tag == "Rod";
    }
    #endregion
}