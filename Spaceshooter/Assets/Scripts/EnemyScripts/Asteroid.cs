using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour, Enemy
{
    [SerializeField] private Vector2 minMaxSpeed;
    [SerializeField] private float maxRotationSpeed;
    [SerializeField] private int hp = 1;
    private bool dead = false;
    private Vector3 _rotationSpeed;
    private float _speed;
    [SerializeField] private Vector2 minMaxScale;

    public GameObject explosionPrefab;
    [SerializeField] private int power;

    void Start()
    {
        SetSpeedPosition();
    }

    void Update()
    {
        float amtToMove = Time.deltaTime * _speed;
        transform.Translate(Vector3.down * amtToMove, Space.World);
        
        Vector3 amtToRotate = Time.deltaTime * _rotationSpeed;
        transform.Rotate(amtToRotate);
    }
    public void SetSpeedPosition()
    {
        _speed = Random.Range(minMaxSpeed.x, minMaxSpeed.y);
        
        float scale = Random.Range(minMaxScale.x, minMaxScale.y);
        transform.localScale = Vector3.one * scale;
        
        _rotationSpeed.x = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        _rotationSpeed.y = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        _rotationSpeed.z = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        
        Vector3 screenPos = Camera.main.ViewportToWorldPoint( 
            new Vector3(Random.Range(0.0f,1.0f), 1, 0));
        
        transform.position = new Vector3(screenPos.x, screenPos.y, transform.position.z);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!dead && other.CompareTag("PlayerProjectile") || other.CompareTag("Player"))
        {
            hp--;
            if (hp == 0)
            {
                dead = true;
                Instantiate(explosionPrefab, transform.position, transform.rotation);
                Player.Score += power;
                Destroy(gameObject);
            }
        }
    }

    public int GetPower()
    {
        return power;
    }
}
