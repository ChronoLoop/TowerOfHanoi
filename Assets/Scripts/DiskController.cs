using System;
using UnityEngine;

public class DiskController : MonoBehaviour
{
    [SerializeField] private Collider diskCollider;
    [SerializeField] private Rigidbody rb;
    public int size { get; set; }
    public Color color { get; set; }
    //currentRod will be set by RodController
    public RodController currentRod { get; set; }
    public bool shouldPlayDropSound { get; set; }
    private RodController previousRod;
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
        shouldPlayDropSound = true;
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
            GameObject rodGameObj = other.gameObject;
            RodController rodController = rodGameObj.GetComponent<RodController>();
            if (rodController.CanAddDisk(this))
            {
                rodController.AddDiskToRodStack(this);
                currentRod = rodController;
                isOnRod = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isRod(other.gameObject))
        {
            //disk is not on a rod
            if (currentRod == null)
            {
                isOnRod = false;
            }
            //check if disk is exiting its current rod
            else if (currentRod == other.gameObject.GetComponent<RodController>())
            {
                currentRod.RemoveDiskFromRodStack();
                currentRod = null;
                isOnRod = false;
            }
        }
    }
    #endregion


    #region Drag and Drop functions
    //reference for drag and drop: https://www.youtube.com/watch?v=0yHBDZHLRbQ
    private void OnMouseDown()
    {
        //Debug.Log("mouse down: " + this.name);
        if (CanDragDisk())
        {
            diskCollider.isTrigger = true;
            mouseZCoord = Camera.main.WorldToScreenPoint(transform.position).z;
            mouseOffset = transform.position - GetMouseWorldPos();
            diskPositionBeforeMouseClick = transform.position;
            isBeingDragged = true;
            previousRod = currentRod;
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
            shouldPlayDropSound = false;
        }
    }
    private void OnMouseUp()
    {
        if (!GameManager.gameIsPaused)
        {
            //if disk is not on Rod, reset the disks position to diskPositionBeforeMouseClick
            if ((!isOnRod && isBeingDragged) || !currentRod)
            {
                isOnRod = false;
                transform.position = diskPositionBeforeMouseClick;
                shouldPlayDropSound = false;
                //Debug.Log("resetting position of disk");
            }
            else if (isOnRod && isBeingDragged)
            {
                //if disk is added to a new rod
                if (currentRod != previousRod)
                {
                    currentRod.DiskDropped();
                    shouldPlayDropSound = true;
                }
                AdjustDiskPositon(this.gameObject);
            }
            diskCollider.isTrigger = false;
            isBeingDragged = false;
        }
    }
    #endregion

    #region Helper Functions
    private bool CanDragDisk()
    {
        //TODO: check if this disk is the topmost disk and game is not paused
        if (isOnRod && (currentRod.GetTopDisk().size == this.size) && !GameManager.gameIsPaused && GameManager.gameHasStarted)
        {
            return true;
        }
        else
        {
            //Debug.Log("isOnRod: " + isOnRod);
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

    public bool isDiskMoving()
    {
        //if velocity is very low, assume it's not moving
        Vector3 velocity = new Vector3(
            (float)Math.Round(rb.velocity.x, 0),
            (float)Math.Round(rb.velocity.y, 0),
            (float)Math.Round(rb.velocity.z, 0)
        );
        if (velocity != Vector3.zero)
        {
            return true;
        }
        return false;
    }


    private void AdjustDiskPositon(GameObject diskGameObj)
    {
        //subtract 1 because Rod will have added this disk when this function is called
        int diskCount = currentRod.GetDiskCount() - 1;

        float draggedDiskYPosition = diskGameObj.transform.position.y;
        float diskYScale = diskGameObj.transform.localScale.y;
        float diskStackTopYPosition = currentRod.GetDiskStackTopYPosition(diskYScale, diskCount);
        //Debug.Log("diskStackTopPostion: " + diskStackTopYPosition + diskYScale);
        float draggedDiskBottomYPosition = draggedDiskYPosition - diskYScale;

        if (draggedDiskBottomYPosition < diskStackTopYPosition)
        {
            //Debug.Log("diskpostion is lower than stack top position");
            draggedDiskYPosition = diskStackTopYPosition + diskYScale;
            //Debug.Log("Reposition to " + draggedDiskYPosition);
        }
        diskGameObj.transform.position = new Vector3(
            diskGameObj.transform.position.x,
            draggedDiskYPosition,
            diskGameObj.transform.position.z
        );
    }
    #endregion
}