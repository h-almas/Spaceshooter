using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBubble : PowerUp
{
    [SerializeField] private GameObject shieldBubblePrefab;
    private bool active;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ApplyEffect(other.GetComponent<Player>()));
        }
    }

    private IEnumerator ApplyEffect(Player player)
    {
        DisableColliderAndRenderer();

        Player.State previous = player._playerState == Player.State.Godmode ? Player.State.Godmode : Player.State.Playing;
        player._playerState = Player.State.Shielded;
        GameObject bubble = Instantiate(shieldBubblePrefab, player.transform);
        
        yield return new WaitForSeconds(duration*0.75f);
        active = true;
        StartCoroutine(Blink(bubble.GetComponentInChildren<Renderer>()));
        yield return new WaitForSeconds(duration * 0.25f);
        active = false;
        
        Destroy(bubble);
        if(player._playerState==Player.State.Shielded)
            player._playerState = previous;
        Destroy(gameObject);
    }

    private IEnumerator Blink(Renderer renderer)
    {
        while (active)
        {
            renderer.enabled = false;
            yield return new WaitForSeconds(duration * 0.0375f);
            renderer.enabled = true;
            yield return new WaitForSeconds(duration * 0.075f);
        }
    }
}
