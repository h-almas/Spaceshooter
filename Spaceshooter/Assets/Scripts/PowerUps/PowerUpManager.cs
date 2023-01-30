using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private GameObject[] powerUps;
    private int lastPowerUpIndex;
    [SerializeField] private float spawnEveryXSeconds = 20f;
    private float timeSinceLastSpawn = 0;
    
    void Update()
    {
        if (timeSinceLastSpawn > spawnEveryXSeconds)
        {
            SpawnRandomPowerUp();
            timeSinceLastSpawn = 0;
        }
        else
        {
            timeSinceLastSpawn += Time.deltaTime;
        }
    }


    public void SpawnRandomPowerUp()
    {
        if(powerUps.Length>0)
            Instantiate(GetPowerUp(), Camera.main.ViewportToWorldPoint(
                new Vector3(Random.Range(0.0f, 1.0f), 1.1f, 10)), Quaternion.identity);
    }
    
    public void DropRandomPowerUp(float dropChance, Vector3 positon)
    {
        if(powerUps.Length>0 && Random.Range(0f,1f)<dropChance)
            Instantiate(GetPowerUp(), positon, Quaternion.identity);
    }

    private GameObject GetPowerUp()
    {
        int nextPowerUpIndex = 0;
        float random = 0;
        do{
            random = Random.Range(0f, 1f);
            nextPowerUpIndex = Random.Range(0, powerUps.Length);

        } while (nextPowerUpIndex == lastPowerUpIndex || 
                 random > 1 / (powerUps[nextPowerUpIndex].GetComponent<PowerUp>().rarity*2));

        lastPowerUpIndex = nextPowerUpIndex;


        return powerUps[nextPowerUpIndex];
    }

}
