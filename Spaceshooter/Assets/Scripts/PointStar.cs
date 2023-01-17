using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointStar : MonoBehaviour
{
    public GameObject disappear;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(disappear, transform.position, transform.rotation);
            Player.Score += 10;
            PlayerBonus.stars++;
            Destroy(gameObject);
        }
    }
}
