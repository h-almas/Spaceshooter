using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointStar : MonoBehaviour
{
    public GameObject disappear;
    private Camera _mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        var positionInViewSpace = _mainCamera.WorldToScreenPoint(transform.position);
        
        if (positionInViewSpace.x < -9)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(disappear, transform.position, transform.rotation);
            Player.Score += 2;
            PlayerBonus.stars++;
            PlayerPrefs.SetInt("FinalStars", PlayerPrefs.GetInt("FinalStars",0) + 1);
            Destroy(gameObject);
        }
    }
}
