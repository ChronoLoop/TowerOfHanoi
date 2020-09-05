using System;
using System.Collections.Generic;
using UnityEngine;

public class RodController : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;
    private Stack<DiskController> diskStack = new Stack<DiskController>();

    public delegate void DiskDropEventHandler(object src, EventArgs e);
    public event DiskDropEventHandler DiskDropSuccessfulEvent;

    #region Disk Drop 
    protected virtual void OnDiskDrop()
    {
        if (DiskDropSuccessfulEvent != null)
        {
            DiskDropSuccessfulEvent(this, EventArgs.Empty);
        }
    }
    public void DiskDropped()
    {
        OnDiskDrop();
    }
    #endregion

    private void Update()
    {
        CheckToPlayDiskSound();
    }

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
            if (disk.isDiskMoving())
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
            //disk stack is 0
            return true;
        }
        else if (disk.size < GetTopDisk().size)
        {
            //disk is smaller
            return true;
        }
        //disk is bigger
        return false;
    }

    private void CheckToPlayDiskSound()
    {
        foreach (DiskController disk in diskStack)
        {
            if (!disk.isDiskMoving() && disk.shouldPlayDropSound)
            {
                soundManager.PlayDiskDropSoundEffect();
                disk.shouldPlayDropSound = false;
            }
        }
    }
    #endregion
}