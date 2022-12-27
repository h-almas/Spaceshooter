using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector2 minMaxSpeed = new Vector2(5f, 10f);
    [SerializeField] private Vector2 minMaxScale = new Vector2(.5f,1.5f);
    [SerializeField] private float maxRotation = 5f;
    [SerializeField] private ParticleSystem explosion;
    private float speed = 0f;
    private float rotation;
    private float scale;
    private bool hit;
    
    // Start is called before the first frame update
    void Start()
    {
        
        SetSpeedAndPosition();
    }

    // Update is called once per frame
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
        hit = false;
    }
    
    private void OnBecameInvisible()
    {
        SetSpeedAndPosition();
    }

    public void incSpeed()
    {
        minMaxSpeed.x += 0.25f;
        minMaxSpeed.y += 0.25f;
    }
    
    public void OnHit()
    {
        if (!hit)
        {
            hit = true;
            incSpeed();
            Instantiate(explosion, transform.position, transform.rotation);
            SetSpeedAndPosition();
        }
    }
}


