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

    [Header ("Events")]
    public static UnityEvent onEnemydestroy = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;

    private bool isSpawning = false;

    private void Start(){
        StartCoroutine(StartWave());
    }

    private void Awake(){
        onEnemydestroy.AddListener(EnemyDestroyed);
    }


    void Update()
    {
        if(!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;
        if(timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0){
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        Debug.Log("Enemies ALIVE" + enemiesAlive);
        Debug.Log("Enemies LEFT" + enemiesLeftToSpawn);

        if(enemiesAlive == 0 && enemiesLeftToSpawn == 0){
            EndWave();
        }
    }


    private void EnemyDestroyed()
    {
        if(enemiesAlive > 0) enemiesAlive--;
    }
        


    private void SpawnEnemy()
    {
        System.Random rnd = new System.Random();
        GameObject prefabToSpwan = enemyPrefabs[rnd.Next(0, enemyPrefabs.Length)];
        Instantiate(prefabToSpwan, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);

        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        LevelManager.main.IncreaseWaves();
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());

    }

    private int EnemiesPerWave(){
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    // Update is called once per frame
    
}
