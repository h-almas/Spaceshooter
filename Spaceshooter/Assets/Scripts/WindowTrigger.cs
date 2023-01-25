using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (CompareTag("Player"))
        {
            Debug.Log("xD");
        }
    }
}
