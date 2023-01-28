using System;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [SerializeField] private float speed = 1.5f;
    [SerializeField] protected float duration = 5f;
    
    private void Update()
    {
        transform.Translate(Vector3.down * (speed*Time.deltaTime));
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
