using System;
using System.Collections.Generic;
using UnityEngine;

public class RodController : MonoBehaviour
{
    private Stack<DiskController> diskStack = new Stack<DiskController>();

    #region Trigger
    private void OnTriggerEnter(Collider other)
    {
        if (IsADisk(other.gameObject))
        {
            GameObject diskGameObj = other.gameObject;
            AdjustDiskPositon(diskGameObj);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (IsADisk(other.gameObject))
        {
            GameObject diskGameObj = other.gameObject;
            AdjustDiskPositon(diskGameObj);
        }
    }
    #endregion

    #region rod stack functions
    public DiskController GetTopDisk()
    {
        if (diskStack.Count > 0)
        {
            return diskStack.Peek();
        }
        return null;
    }
    public int GetDiskCount()
    {
        return diskStack.Count;
    }
    public void ClearStack()
    {
        diskStack.Clear();
    }
    public void AddDiskToRodStack(DiskController disk)
    {
        diskStack.Push(disk);
    }
    public void RemoveDiskFromRodStack()
    {
        DiskController poppedDisk = diskStack.Pop();
        if (poppedDisk.currentRod == this)
        {
            poppedDisk.currentRod = null;
        }
    }
    #endregion

    #region helper functions
    private bool IsADisk(GameObject obj)
    {
        return obj.tag == "Disk";
    }
    public bool AreDisksMoving()
    {
        bool disksMoving = false;
        //if a disk is moving return true
        foreach (DiskController disk in diskStack)
        {
            //if velocity is very low, it's basically not moving
            Vector3 velocity = new Vector3(
                (float)Math.Round(disk.rb.velocity.x, 0),
                (float)Math.Round(disk.rb.velocity.y, 0),
                (float)Math.Round(disk.rb.velocity.z, 0)
            );

            if (velocity != Vector3.zero)
            {
                disksMoving = true;
                break;
            }
        }
        return disksMoving;
    }

    private void AdjustDiskPositon(GameObject diskGameObj)
    {
        diskGameObj.transform.position = new Vector3(
            transform.position.x,
            diskGameObj.transform.position.y,
            diskGameObj.transform.position.z
        );
    }
    private float GetBottomYPositionOfRod()
    {
        return transform.position.y - transform.localScale.y;
    }

    public float GetDiskStackTopYPosition(float diskYScale, int diskCount)
    {
        return (diskYScale * 2 * diskCount) + GetBottomYPositionOfRod();
    }
    public bool CanAddDisk(DiskController disk)
    {
        if (diskStack.Contains(disk))
        {
            return false;
        }
        else if (GetDiskCount() == 0)
        {
            //Debug.Log("diskStack is 0");
            return true;
        }
        else if (disk.size < GetTopDisk().size)
        {
            //Debug.Log("disk is smaller");
            return true;
        }
        //Debug.Log("disk is bigger");
        return false;
    }
    #endregion
}