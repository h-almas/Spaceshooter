using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissedEnemies : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI missedText;
    private float missedEnemies;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            missedEnemies++;
            missedText.text = "Missed Hits " + missedEnemies;
        }
    }
}
