using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowTrigger : MonoBehaviour
{
    [SerializeField] private Transform statsWindow;
    [SerializeField] private Transform playStats;
    public GameObject score;
    public GameObject stars;
    private void Awake()
    {
        statsWindow.localScale = Vector2.zero;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playStats.LeanScale(Vector2.zero, 0.8f);
            statsWindow.LeanScale(Vector2.one, 0.8f);
            StartCoroutine(ShowStats());
        }
    }

    private IEnumerator ShowStats()
    {
        yield return new WaitForSeconds(2);
        score.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        stars.SetActive(true);
    }
}
