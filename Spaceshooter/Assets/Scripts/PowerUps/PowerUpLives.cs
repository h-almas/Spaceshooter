using UnityEngine;

public class PowerUpLives : PowerUp
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().IncLives();
            Destroy(gameObject);
        }
    }
}
