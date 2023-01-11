using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<Wave> waves;


    public void Start()
    {
        StartCoroutine(StartWaves());
    }

    public IEnumerator StartWaves()
    {
        for(int i = 0; i<waves.Count; i++)
        {
            yield return new WaitForSeconds(2.5f);          // Wait before showing warning
            Debug.Log("Wave " + i + " starting!");   // Display Wave warning here
            yield return new WaitForSeconds(2.5f);          // Wait a little longer before starting
            
            StartCoroutine(waves[i].StartWave());
            yield return new WaitUntil(() => waves[i].HasEnded());
        }
    }


    [Serializable]
    public class Wave
    {
        [SerializeField] private List<GameObject> enemyPrefabs = new List<GameObject>();
        [SerializeField] private int wavePower;
        [SerializeField] private int maxPowerOnScreen;
        private bool hasEnded = false;


        public IEnumerator StartWave()
        {
            
            while (wavePower > 0)
            {

                yield return new WaitForSeconds(0.5f);


                var enemiesOnScreen = FindObjectsOfType<MonoBehaviour>().OfType<Enemy>();

                int currentPowerOnScreen = 0;
                
                foreach (Enemy enemy in enemiesOnScreen)
                {
                    currentPowerOnScreen += enemy.GetPower();
                }
                
                
                if (currentPowerOnScreen <= maxPowerOnScreen)
                {
                    int randomEnemyType = Random.Range(0, enemyPrefabs.Count);
                    
                    //TODO: .cost on non existing object leads to nullreferenceexception. Without the following code on screen power might exceed maxPower
                    /*while (enemyPrefabs[randomEnemyType].GetComponent<Enemy>().cost + currentPowerOnScreen > maxPowerOnScreen)
                    {
                        randomEnemyType = Random.Range(0, enemyPrefabs.Count);
                    }*/

                    Enemy enemy = Instantiate(enemyPrefabs[randomEnemyType]).GetComponent<Enemy>();
                    wavePower -= enemy.GetPower();

                }

                
                
            }

            yield return new WaitUntil(() => FindObjectsOfType<MonoBehaviour>().OfType<Enemy>().Count()==0);
            
            Debug.Log("Wave completed!");
            hasEnded = true;
        }

        public bool HasEnded()
        {
            return hasEnded;
        }
        
    }
}
