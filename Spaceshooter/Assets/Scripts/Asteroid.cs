using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, Enemy
{
    [SerializeField] private Vector2 minMaxSpeed = new Vector2(5f, 10f);
    [SerializeField] private Vector2 minMaxScale = new Vector2(.8f,1.2f);
    [SerializeField] private float maxRotation = 5f;
    [SerializeField] private ParticleSystem explosion;
    private float speed = 0f;
    private float rotation;
    private float scale;
    private bool hit;
    public int power = 3;

    void Start()
    {
        SetSpeedAndPosition();
    }

    void Update()
    {
        
        float amtToMove = Time.deltaTime * speed;
        transform.Translate(Vector3.down * amtToMove, Space.World);
        
        transform.Rotate(Vector3.forward, rotation*Time.deltaTime, Space.Self);
        
    }

    public void SetSpeedAndPosition()
    {
        scale = Random.Range(minMaxScale.x, minMaxScale.y);
        transform.localScale = Vector3.one * scale;
        
        rotation = Random.Range(-maxRotation, maxRotation);
        
        
        speed = Random.Range(minMaxSpeed.x, minMaxSpeed.y);
        if(Camera.main!=null)
            transform.position = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), 1f, 10.25f));
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Projectile")) return;
        
        if (!hit)
        {
            hit = true;
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public int GetPower()
    {
        return power;
    }
    
}
