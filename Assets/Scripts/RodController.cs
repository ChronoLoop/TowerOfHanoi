using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodController : MonoBehaviour
{
    Stack<DiskController> diskStack = new Stack<DiskController>();

    #region Trigger
    private void OnTriggerEnter(Collider other)
    {
        if (IsADisk(other.gameObject))
        {
            GameObject disk = other.gameObject;
            disk.transform.position = new Vector3(
                transform.position.x,
                disk.transform.position.y,
                disk.transform.position.z
            );
            DiskController diskController = disk.GetComponent<DiskController>();
            //add diskcontroller script to dictionary
            if (!diskStack.Contains(diskController))
            {
                diskStack.Push(diskController);
                diskController.currentRod = this;
                //print(this.name + " added " + disk.name);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (IsADisk(other.gameObject))
        {
            GameObject disk = other.gameObject;
            disk.transform.position = new Vector3(
                transform.position.x,
                disk.transform.position.y,
                disk.transform.position.z
            );
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (IsADisk(other.gameObject))
        {
            GameObject disk = other.gameObject;
            DiskController poppedDisk = diskStack.Pop();
            poppedDisk.currentRod = null;
            //print(this.name + ": has removed disk");
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
            if (disk.rb.velocity != Vector3.zero)
            {
                disksMoving = true;
                break;
            }
        }
        return disksMoving;
    }
    public int GetDiskCount()
    {
        return diskStack.Count;
    }
    public void ClearStack()
    {
        diskStack.Clear();
    }
    public DiskController GetTopDisk()
    {
        if (diskStack.Count > 0)
        {
            return diskStack.Peek();
        }
        return null;
    }
    #endregion
}