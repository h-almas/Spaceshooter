using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyShip : MonoBehaviour, Enemy
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int power = 20;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float timeBetweenShots = .5f;
    private float timeSinceLastShot;
    private Vector3 direction = Vector3.right;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), 1.25f, 10) );
        transform.position = pos;
        StartCoroutine(MoveToCenterOfPath());
    }

    // Update is called once per frame
    void Update()
    {
        #region Move

        Vector3 posInView = Camera.main.WorldToViewportPoint(transform.position);

        if (posInView.x > 1 ||
            posInView.x < 0)
        {
            direction *= -1;
        }
            
        transform.Translate(direction*(speed*Time.deltaTime));

        #endregion

        #region Shoot

        if (timeSinceLastShot >= timeBetweenShots)
        {
            Instantiate(projectilePrefab, muzzle.position, Quaternion.identity);
            timeSinceLastShot = 0;
        }
        else
        {
            timeSinceLastShot += Time.deltaTime;
        }

        #endregion
    }

    private IEnumerator MoveToCenterOfPath()
    {
        float centerY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.75f, 0)).y;
        float originY = transform.position.y;
        float t = 0;
        float speed = 0.5f;
        
        while (Math.Abs(transform.position.y-centerY) > 0.1f)
        {
            float y = Mathf.Lerp(originY, centerY, t);
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
            t += speed * Time.deltaTime;
            yield return new WaitForNextFrameUnit();
        }
    }

    public int GetPower()
    {
        return power;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerProjectile") || other.CompareTag("Player"))
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            Player.Score += power;
            Destroy(gameObject);
        }
    }
}