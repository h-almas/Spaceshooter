using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    
    [SerializeField] public WaveList<Wave> waveList = new WaveList<Wave>();

    private IntermissionWindow _intermissionWindow;
    private void Awake()
    {
        _intermissionWindow = GetComponent<IntermissionWindow>();
    }

    public void Start()
    {
        StartCoroutine(StartWaves());
    }

    public IEnumerator StartWaves()
    {
        for(int i = 0; i<waveList.Count; i++)
        {
            StartCoroutine(waveList[i].StartWave());
            yield return new WaitUntil(() => waveList[i].HasEnded());
        }
        
        //Start EndSequence here

        yield return new WaitForSeconds(3);

        //PlayerPrefs.SetInt("OverallScore", PlayerPrefs.GetInt("OverallScore",0) + Player.Score);
        
        _intermissionWindow.OpenWindow();
    }


    [Serializable]
    public class Wave
    {
        [SerializeField] protected float delayBetweenSpawns = .5f;
        [SerializeField] private float delayBeforeWaveWarning = 2.5f;
        [SerializeField] private string waveWarning = "Wave starting";
        [SerializeField] protected float delayBeforeWaveStart = 2.5f;
        
        [SerializeField] protected bool useCount = false;
        [SerializeField] protected List<GameObject> enemyPrefabs = new List<GameObject>();

        [SerializeField] protected int wavePower;
        [SerializeField] protected int maxPowerOnScreen;
        [SerializeField] protected int enemyCount;
        [SerializeField] protected int maxEnemiesOnScreen;
        protected bool hasEnded = false;
        

        public IEnumerator StartWave()
        {

            yield return new WaitForSeconds(delayBeforeWaveWarning);
            
            if (waveWarning != "")
            {
                FindObjectOfType<WarningMessage>().DisplayMessage(waveWarning);
            }
            
            yield return new WaitForSeconds(delayBeforeWaveStart);
            
            int remaining = useCount ? enemyCount : wavePower;
            int max = useCount ? maxEnemiesOnScreen : maxPowerOnScreen;
                
            while (remaining > 0)
            {

                yield return new WaitForSeconds(delayBetweenSpawns);


                var enemiesOnScreen = FindObjectsOfType<MonoBehaviour>().OfType<Enemy>();

                int onScreen = 0;

                foreach (Enemy enemy in enemiesOnScreen)
                {
                    onScreen += useCount ? 1 : enemy.GetPower();
                }


                if (onScreen < max)
                {
                    int randomEnemyType = Random.Range(0, enemyPrefabs.Count);

                    //TODO: .cost on non existing object leads to nullreferenceexception. Without the following code on screen power might exceed maxPower
                    /*while (enemyPrefabs[randomEnemyType].GetComponent<Enemy>().cost + currentPowerOnScreen > maxPowerOnScreen)
                    {
                        randomEnemyType = Random.Range(0, enemyPrefabs.Count);
                    }*/

                    Enemy enemy = Instantiate(enemyPrefabs[randomEnemyType]).GetComponent<Enemy>();
                    remaining -= useCount ? 1 : enemy.GetPower();

                }
                    
            }

            yield return new WaitUntil(() => !FindObjectsOfType<MonoBehaviour>().OfType<Enemy>().Any());
            
            
            Debug.Log("Wave completed!");
            hasEnded = true;
        }

        public bool HasEnded()
        {
            return hasEnded;
        }

        
    }

    [Serializable]
    public class WaveList<Wave> //Wrapper to make PropertyDrawer easier
    {
        [SerializeField] private List<Wave> waves;
        
        public Wave this[int i]
        {
            get => waves[i];
            //set => waves[i] = value;
        }

        public int Count => waves.Count;

        /*public void Insert(int index, Wave item)
        {
            waves.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            waves.RemoveAt(index);
        }

        public void Move(int from, int to)
        {
            Wave item = waves[from];
            RemoveAt(from);
            Insert(to, item);
        }

        public void clearSubwaves()
        {
            
            if (typeof(Wave) is Wave)
            {
                foreach (Wave w in waves)
                {
                    w.ClearSubwaves();
                }
            }
        }*/
        
    }
}