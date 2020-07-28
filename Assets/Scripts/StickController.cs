using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickController : MonoBehaviour
{
    Stack<DiskController> _disks = new Stack<DiskController>();
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
    }
}