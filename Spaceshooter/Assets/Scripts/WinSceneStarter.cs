using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSceneStarter : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadeImage;
    [SerializeField] private GameObject scoreFinal;
    [SerializeField] private GameObject livesFinal;
    [SerializeField] private GameObject starsFinal;
    [SerializeField] private GameObject missedHitsFinal;
    [SerializeField] private GameObject buttons;

    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    [SerializeField] private TMPro.TextMeshProUGUI livesText;
    [SerializeField] private TMPro.TextMeshProUGUI starsText;
    [SerializeField] private TMPro.TextMeshProUGUI missedHitsText;

    void Start()
    {
        scoreText.text = "SCORE: " + Player.Score;
        livesText.text = "LIVES: " + Player.lives;
        starsText.text = "STARS: " + PlayerBonus.stars;
        missedHitsText.text = "MISSED HITS: " + MissedEnemies.missedHits;
        fadeImage.LeanAlpha(0, 2);
        StartCoroutine(FinalStats());
    }

    private IEnumerator FinalStats()
    {
        yield return new WaitForSeconds(4);
        scoreFinal.SetActive(true);
        yield return new WaitForSeconds(1.6f);
        livesFinal.SetActive(true);
        yield return new WaitForSeconds(1.6f);
        starsFinal.SetActive(true);
        yield return new WaitForSeconds(1.6f);
        missedHitsFinal.SetActive(true);
        yield return new WaitForSeconds(4);
        buttons.SetActive(true);
    }
}
