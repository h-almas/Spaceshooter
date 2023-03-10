using System;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class ChasingKamikazeRocket : Enemy
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float steeringSpeed = 5;
    [SerializeField] private float maxAngle = 45f;
    private Transform target;
    
    void Start()
    {
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(.05f, .95f), 1.5f, 10) );
        transform.position = pos;
        target = FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        var towardsTarget = target.position - transform.position;
        var facing = transform.up * -1;
        var angle = Vector3.SignedAngle(facing, towardsTarget, Vector3.forward);
        if (Mathf.Abs(angle) < maxAngle)
            transform.Rotate(Vector3.forward, angle * steeringSpeed * Time.deltaTime);
        
        transform.Translate(Vector3.down*(speed*Time.deltaTime), Space.Self);

    }
}