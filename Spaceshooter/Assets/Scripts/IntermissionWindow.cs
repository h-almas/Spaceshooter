using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermissionWindow : MonoBehaviour
{
    [SerializeField] private Transform statsWindow;
    [SerializeField] private Transform playStats;
    [SerializeField] private Transform missedHitStats;
    
    [SerializeField] private GameObject score;
    [SerializeField] private GameObject lives;
    [SerializeField] private GameObject missedHits;


    private void Awake()
    {
        statsWindow.localScale = Vector2.zero;
    }

    public void OpenWindow()
    {
        playStats.LeanScale(Vector2.zero, 0.8f);
        missedHitStats.LeanScale(Vector2.zero, 0.8f);
        statsWindow.LeanScale(Vector2.one, 0.8f);
        StartCoroutine(ShowStats());
    }

    private IEnumerator ShowStats()
    {
        yield return new WaitForSeconds(2);
        score.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        lives.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        missedHits.SetActive(true);
    }
}
