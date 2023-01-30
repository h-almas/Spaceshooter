using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float steeringSpeed = 5;
    [SerializeField] private float maxDistance = 3;
    [SerializeField] protected float damage;
    private Transform target = null;
    
    
    void Update()
    {
        if (target == null || Vector3.Distance(target.position, transform.position) > maxDistance)
        {
            target = null;
            var enemies = FindObjectsOfType<GameObject>();
            if (enemies.Length != 0)
            {
                GameObject closest = null;
                foreach (GameObject g in enemies)
                {
                    if (closest == null && Vector3.Distance(transform.position, g.transform.position)<maxDistance && g.CompareTag("Enemy"))
                    {
                        closest = g;
                        continue;
                    }
                    
                    if (Vector3.Distance(transform.position, g.transform.position)<maxDistance && g.CompareTag("Enemy") && Vector3.Distance(g.transform.position, transform.position) <
                        Vector3.Distance(closest.transform.position, transform.position))
                    {
                        closest = g;
                    }
                }

                if(closest!=null)
                    target = closest.transform;
            }
        }
        

        if (target != null)
        {
            var towardsTarget = target.position - transform.position;
            var facing = transform.up;
            var angle = Vector3.SignedAngle(facing, towardsTarget, Vector3.forward);
            transform.Rotate(Vector3.forward, angle * steeringSpeed * Time.deltaTime);
        }
        
        
        transform.Translate(Vector3.up*(speed*Time.deltaTime), Space.Self);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().DealDamage(damage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("EnemyProjectile"))
        {
            Destroy(gameObject);
        }
    }
}
