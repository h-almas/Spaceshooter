using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpProjectile : PowerUp
{
    [SerializeField] protected GameObject projectilePrefab;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<WarningMessage>().DisplayMessage(message);
            StartCoroutine(ApplyEffect(other.GetComponent<Player>()));
        }
    }

    private IEnumerator ApplyEffect(Player player)
    {
        DisableColliderAndRenderer();
        
        player.CurrentProjectile = projectilePrefab;
        
        yield return new WaitForSeconds(duration);

        if(player.CurrentProjectile==projectilePrefab)
            player.CurrentProjectile = player.baseProjectile;
        
        Destroy(gameObject);
    }
}
