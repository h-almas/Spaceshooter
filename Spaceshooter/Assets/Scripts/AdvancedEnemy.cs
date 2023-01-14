using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedEnemy : MonoBehaviour
{
    public enum Behaviour
    {
        Chase,
        Intercept,
        Patrol,
        ChasePatrol,
        Hide
    }

    public float chaseSpeed;
    public float normalSpeed;
    public Rigidbody prey;
    public Behaviour behaviour;
    public Rigidbody enemyRigidbody;

    private void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        switch (behaviour)
        {
            case Behaviour.Chase: Chase(prey.position, chaseSpeed);
                break;
            case Behaviour.Intercept: Intercept(prey.position);
                break;
        }
    }

    private void Chase(Vector3 targetPosition, float speed)
    {
        enemyRigidbody.velocity = (targetPosition - transform.position) * speed;
    }

    private void Intercept(Vector3 targetPosition)
    {
        var relativeVelocity = prey.velocity - enemyRigidbody.velocity;
        var distance = Vector3.Distance(targetPosition, transform.position);
        var timeToClose = distance / relativeVelocity.magnitude;
        var interceptionPoint = targetPosition + prey.velocity * timeToClose;
        
        Chase(interceptionPoint, chaseSpeed);
    }
}
