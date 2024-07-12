using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
     [SerializeField] private float SpawningSpeedScalingFactor = 0.1f;

    [Header ("Events")]
    public static UnityEvent onEnemydestroy = new UnityEvent();

     private Coroutine coroutine;
     private Coroutine startWaveCoroutine;
     private Coroutine runTutorialCoroutine;

    public static bool isFirstEnemySpawned = false;
    private int currentWave;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;

    private static bool isFirstEnemyKilled = false;
    private static bool isFirstWaveEnded = false;
    private bool isSpawning = false;
 
    private void Start(){
        currentWave = LevelManager.main.GetCurrentWave();
        coroutine = StartCoroutine(StartWave());
    }

    private void Awake(){
        onEnemydestroy.AddListener(EnemyDestroyed);
    }

    public static void SetisFirstWaveEnded(bool b){
        isFirstWaveEnded = b;
    }
    public static void SetisFirstEnemyKilled(bool b){
        isFirstEnemyKilled = b;
    }

    public static bool GetisFirstEnemyKilled(){
        return isFirstEnemyKilled;
    }


    void Update()
    {
        if(!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;
        if(timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0){
            Debug.Log("SPAWNING ENEMY ON WAVE: " + currentWave + " LEFT TO SPWAN: " + enemiesLeftToSpawn);
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if(enemiesAlive == 0 && enemiesLeftToSpawn == 0){
             if(currentWave == 1){
                    if(!isFirstWaveEnded){
                         TutorialManager.instance.RunTutorialStep();
                         isFirstWaveEnded = true;   
                    }
                   
                }
            EndWave();
        }
    }


    private void EnemyDestroyed()
    {
        if(enemiesAlive > 0) enemiesAlive--;
    }
        


    private void SpawnEnemy()
    {
        if(LevelManager.main == null) return;
        System.Random rnd = new System.Random();
        GameObject prefabToSpwan = enemyPrefabs[rnd.Next(0, enemyPrefabs.Length)];
        Instantiate(prefabToSpwan, LevelManager.main.startPoint.position, Quaternion.identity);
        if (!isFirstEnemySpawned){
            runTutorialCoroutine = StartCoroutine(PauseAfterEnemySpawn(0.25f)); 
            isFirstEnemySpawned = true;
        }
    }

    private IEnumerator PauseAfterEnemySpawn(float delay){
        yield return new WaitForSeconds(delay); 
        TutorialManager.instance.RunTutorialStep(); 

    }

    private void OnDestroy()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        if (startWaveCoroutine != null)
        {
            StopCoroutine(startWaveCoroutine);
        }
        if (runTutorialCoroutine != null)
        {
            StopCoroutine(runTutorialCoroutine);
        }
         // Reset static variables
        isFirstEnemySpawned = false;
        isFirstWaveEnded = false;
        isSpawning = false;
        LevelManager.main.ResetCurrentWave();

        

    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        currentWave = LevelManager.main.GetCurrentWave();
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        enemiesPerSecond = enemiesPerSecond + SpawningSpeedScalingFactor;
        LevelManager.main.IncreaseWaves();
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        LevelManager.main.IncreaseCurrentWave();
        currentWave = LevelManager.main.GetCurrentWave();
        startWaveCoroutine = StartCoroutine(StartWave());

    }

    private int EnemiesPerWave(){
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    // Update is called once per frame
    
}
