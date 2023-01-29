using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBubble : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().GetDamage(5);
        }

        if (other.CompareTag("EnemyProjectile"))
        {
            Destroy(other.gameObject);
        }
    }
}
