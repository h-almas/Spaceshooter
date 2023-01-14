using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    public GameObject explosionPrefab;
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
        Enemy collideWith = other.GetComponent<Enemy>();
        if (collideWith != null)
        {
            Debug.Log("We hit " + other.name);
            Instantiate(explosionPrefab, other.transform.position, other.transform.rotation);
            collideWith.IncreaseSpeed(0.5f);
            collideWith.SetSpeedPosition();
            Player.Score += 10;
            Debug.Log("Your Score is now: " + Player.Score);
            Destroy(gameObject);
        }

        if (Player.Score == 150)
        {
            SceneManager.LoadScene(2);
            Player.Score = 0;
        }
    }
}
