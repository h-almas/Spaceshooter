using UnityEngine;

public class PowerUpLives : PowerUp
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<WarningMessage>().DisplayMessage(message);
            other.GetComponent<Player>().IncLives();
            Destroy(gameObject);
        }
    }
}
