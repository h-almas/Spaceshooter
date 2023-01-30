using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int power;
    [SerializeField] protected float hp;
    protected bool dead = false;
    
    [SerializeField] private GameObject explosionPrefab;

    public virtual int GetPower()
    {
        return power;
    }

    public virtual void DealDamage(float damage)
    {
        if (!dead)
        {
            hp -= damage;
            if (hp <= 0)
            {
                dead = true;
                Die();
            }
        }
    }

    protected virtual void Die()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        FindObjectOfType<PowerUpManager>().DropRandomPowerUp(0.05f, transform.position);
        Player.Score += power;
        Destroy(gameObject);
    }
    
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!dead && other.CompareTag("Player"))
        {
            dead = true;
            Die();
        }
    }
}


