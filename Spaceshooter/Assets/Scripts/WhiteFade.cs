using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WhiteFade : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadeImage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fadeImage.gameObject.SetActive(true);
            fadeImage.LeanAlpha(1, 1);
            PlayerPrefs.SetInt("FinalScore", PlayerPrefs.GetInt("FinalScore",0) + Player.Score);
            StartCoroutine(Transition());
        }
    }

    private IEnumerator Transition()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(7);
    }
}
