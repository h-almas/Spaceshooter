using System.Collections;
using UnityEngine;

public class PowerUpWeapon : PowerUp
{
    [SerializeField] protected GameObject weaponPrefab;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ApplyEffect(other.GetComponent<Player>()));
        }
    }

    private IEnumerator ApplyEffect(Player player)
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        
        GameObject w = Instantiate(weaponPrefab);
        Weapon weapon = w.GetComponent<Weapon>();
        player.CurrentWeapon = weapon;
        

        yield return new WaitForSeconds(duration);

        if(player.CurrentWeapon==weapon)
            player.CurrentWeapon = player.baseWeapon;
        
        Destroy(w);
        Destroy(gameObject);
    }
}
