using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour, Enemy
{
    [SerializeField] private Vector2 minMaxSpeed;
    [SerializeField] private float maxRotationSpeed;
    private Vector3 _rotationSpeed;
    private float _speed;
    [SerializeField] private Vector2 minMaxScale;

    private static int _missedHit;
    public TMPro.TextMeshProUGUI missedText;
    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        SetSpeedPosition();
    }

    // Update is called once per frame
    void Update()
    {
        float amtToMove = Time.deltaTime * _speed;
        transform.Translate(Vector3.down * amtToMove, Space.World);
        
        Vector3 amtToRotate = Time.deltaTime * _rotationSpeed;
        transform.Rotate(amtToRotate);

        if(Camera.main.WorldToViewportPoint(transform.position).y < -0.1f)
        {
            SetSpeedPosition();
        }
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

    public void IncreaseSpeed(float increment)
    {
        if (minMaxSpeed.y > 10f) return;
        minMaxSpeed.x += increment;
        minMaxSpeed.y += increment;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerProjectile"))
        {
            Instantiate(explosionPrefab, other.transform.position, other.transform.rotation);
            IncreaseSpeed(0.5f);
            SetSpeedPosition();
            Player.Score += 10;
            Debug.Log("Your Score is now: " + Player.Score);
            Destroy(gameObject);
        }
        if (other.CompareTag("Missed Trigger"))
        {
            //TODO: the Text field should update itself with the static variable missedHit
            //Prefabs can only save references to other prefabs
            _missedHit++;
            /*missedText.text = "Missed Hits " + _missedHit;*/
            
            Destroy(gameObject);
        }
    }
}
