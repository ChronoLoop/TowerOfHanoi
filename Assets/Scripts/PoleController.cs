using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleController : MonoBehaviour
{
    Stack<DiskController> _disks = new Stack<DiskController>();
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
    private bool IsADisk(GameObject obj)
    {
        return obj.GetComponent<DiskController>() != null;
    }
}