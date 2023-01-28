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
            fadeImage.LeanAlpha(1, 1);
            StartCoroutine(Transition());
        }
    }

    private IEnumerator Transition()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(7);
    }
}
