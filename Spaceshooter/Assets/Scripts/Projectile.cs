using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    private Camera _mainCamera;
    

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float amtToMove = projectileSpeed * Time.deltaTime;
        transform.Translate(Vector3.up * amtToMove);
        
        Vector3 positionInViewSpace = _mainCamera.WorldToViewportPoint(transform.position);
        
        if (positionInViewSpace.y > 1.2f)
        {
            Destroy(gameObject);
        }

    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("We hit " + other.name);
            Player.Score += other.GetComponent<Enemy>().GetPower();
            
            Destroy(gameObject);
        }

        
    }
}
