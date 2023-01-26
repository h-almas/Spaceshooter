using UnityEngine;

public class MissedEnemies : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI missedText;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            PlayerPrefs.SetInt("MissedEnemies", PlayerPrefs.GetInt("MissedEnemies", 0) + 1);
            missedText.text = "Missed Hits " + PlayerPrefs.GetInt("MissedEnemies", 0);
            Destroy(other.gameObject);
        }
    }
}
