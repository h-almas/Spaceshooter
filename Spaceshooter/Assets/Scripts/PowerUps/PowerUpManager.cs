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
            Instantiate(powerUps[Random.Range(0, powerUps.Length)], Camera.main.ViewportToWorldPoint(
                new Vector3(Random.Range(0.0f, 1.0f), 1.1f, 10)), Quaternion.identity);
    }
    
    public void DropRandomPowerUp(float dropChance, Vector3 positon)
    {
        if(powerUps.Length>0 && Random.Range(0f,1f)<dropChance)
            Instantiate(GetPowerUp(), positon, Quaternion.identity);
    }

    private GameObject GetPowerUp()
    {
        int nextPowerUpIndex = Random.Range(0, powerUps.Length);
        if (nextPowerUpIndex == lastPowerUpIndex) 
            nextPowerUpIndex = (nextPowerUpIndex + 1) % powerUps.Length;
        return powerUps[nextPowerUpIndex];
    }

}
