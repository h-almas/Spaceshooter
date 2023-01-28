using UnityEngine;

public class MissedEnemies : MonoBehaviour
{
    public static int missedHits;
    [SerializeField] private TMPro.TextMeshProUGUI missedText;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            missedHits++;
            missedText.text = "Missed Hits " + missedHits;
            Destroy(other.gameObject);
        }
    }
}
