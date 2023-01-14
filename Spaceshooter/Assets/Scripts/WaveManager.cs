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
            StartCoroutine(waves[i].StartWave());
            yield return new WaitUntil(() => waves[i].HasEnded());
        }
    }


    [Serializable]
    public class Wave
    {
        [SerializeField] private float delayBetweenSpawns = .5f;
        [SerializeField] private float delayBeforeWaveWarning = 2.5f;
        [SerializeField] private string waveWarning = "Wave starting";
        [SerializeField] private float delayBeforeWaveStart = 2.5f;
        
        [SerializeField] private bool useSubwaves = false;
        [SerializeField] private bool useCount = false;
        [SerializeField] private List<GameObject> enemyPrefabs = new List<GameObject>();

        [SerializeField] private int wavePower;
        [SerializeField] private int maxPowerOnScreen;
        [SerializeField] private int enemyCount;
        [SerializeField] private int maxEnemiesOnScreen;
        [SerializeField] private List<Wave> subwaves;
        private bool hasEnded = false;
        

        public IEnumerator StartWave()
        {
            if(!useSubwaves){
                yield return new WaitForSeconds(delayBeforeWaveWarning);
                Debug.Log(waveWarning);
                yield return new WaitForSeconds(delayBeforeWaveStart);
            }
            
            
            if (useSubwaves)
            {
                for (int i = 0; i < subwaves.Count; i++)
                {
                    Debug.Log("Subwave " + (i + 1) + " starting!");

                    FindObjectOfType<WaveManager>().StartCoroutine(subwaves[i].StartWave());
                    yield return new WaitUntil(() => subwaves[i].HasEnded());
                }
            }
            else
            {
                int remaining = useCount ? enemyCount : wavePower;
                int max = useCount ? maxEnemiesOnScreen : maxPowerOnScreen;
                
                while (remaining > 0)
                {

                    yield return new WaitForSeconds(delayBetweenSpawns);


                    var enemiesOnScreen = FindObjectsOfType<MonoBehaviour>().OfType<Enemy>();

                    int onScreen = 0;

                    foreach (Enemy enemy in enemiesOnScreen)
                    {
                        /*onScreen += useCount ? 1 : enemy.GetPower();*/
                        onScreen++;
                    }


                    if (onScreen <= max)
                    {
                        int randomEnemyType = Random.Range(0, enemyPrefabs.Count);

                        //TODO: .cost on non existing object leads to nullreferenceexception. Without the following code on screen power might exceed maxPower
                        /*while (enemyPrefabs[randomEnemyType].GetComponent<Enemy>().cost + currentPowerOnScreen > maxPowerOnScreen)
                        {
                            randomEnemyType = Random.Range(0, enemyPrefabs.Count);
                        }*/

                        Enemy enemy = Instantiate(enemyPrefabs[randomEnemyType]).GetComponent<Enemy>();
                        /*remaining -= useCount ? 1 : enemy.GetPower();*/
                        remaining--;

                    }
                    
                }

                yield return new WaitUntil(() => !FindObjectsOfType<MonoBehaviour>().OfType<Enemy>().Any());
            }
            
            Debug.Log("Wave completed!");
            hasEnded = true;
        }

        public bool HasEnded()
        {
            return hasEnded;
        }
    }
}