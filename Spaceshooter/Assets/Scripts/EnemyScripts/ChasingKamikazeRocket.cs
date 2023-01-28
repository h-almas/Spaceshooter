using System;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class ChasingKamikazeRocket : MonoBehaviour, Enemy
{
    [SerializeField] private int power = 20;
    [SerializeField] private float speed = 10;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float steeringSpeed = 5;
    [SerializeField] private float maxAngle = 45f;
    [SerializeField] private int hp = 1;
    private bool dead = false;
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

    public int GetPower()
    {
        return power;
    }

    public void GetDamage(int damage)
    {
        if (!dead)
        {
            hp -= damage;
            if (hp <= 0)
            {
                dead = true;
                Instantiate(explosionPrefab, transform.position, transform.rotation);
                Player.Score += power;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!dead && other.CompareTag("Player"))
        {
            dead = true;
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            Player.Score += power;
            Destroy(gameObject);
        }
    }
}