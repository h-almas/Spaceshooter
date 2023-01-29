using System;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [SerializeField] private float speed = 1.5f;
    [SerializeField] protected float duration = 5f;
    [SerializeField] protected string message;
    
    private void Update()
    {
        transform.Translate(Vector3.down * (speed*Time.deltaTime));
    }

    protected void DisableColliderAndRenderer()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        foreach (var componentsInChild in GetComponentsInChildren<Renderer>())
        {
            componentsInChild.enabled = false;
        }
    }

    private void OnBecameInvisible()
    {
        if(GetComponent<Collider>().enabled) 
            Destroy(gameObject);
    }
}
