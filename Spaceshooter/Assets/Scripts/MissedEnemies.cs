using UnityEngine;

public class MissedEnemies : MonoBehaviour
{
    public int missedHits;
    [SerializeField] private TMPro.TextMeshProUGUI missedText;
    [SerializeField] private TMPro.TextMeshProUGUI missedTextFinal;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            missedHits++;
            PlayerPrefs.SetInt("FinalMissedHits", PlayerPrefs.GetInt("FinalMissedHits", 0) + 1);
            missedText.text = "Missed Hits: " + missedHits;
            missedTextFinal.text = "Missed Hits: " + missedHits;
            Destroy(other.gameObject);
        }
    }
}
