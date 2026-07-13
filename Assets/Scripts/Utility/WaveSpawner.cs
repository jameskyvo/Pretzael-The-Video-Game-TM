using FMODUnity;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using static WaveSpawner;

public class WaveSpawner : MonoBehaviour
{
    public int currentWave;
    public int waveDuration;
    public int secondsBetweenWaves;
    public List<Wave> waves = new List<Wave>();
    public List<GameObject> enemiesToSpawn = new List<GameObject>();

    [HideInInspector]
    public int maxWaves;

    public int horizontalVariance;

    private int remainingEnemies;
    private float secondsUntilNextSpawn;
    private List<Transform> spawnPoints;
    private float spawnInterval = 1;
    private bool isProgressing = false;
    private GameObject player;

    [SerializeField]
    private EventReference newWave;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        spawnPoints = GetAllSpawnPoints();
        maxWaves = waves.Count;
        GenerateWave();
    }

    IEnumerator WaitFornextWave()
    {
        isProgressing = true;

        yield return new WaitForSeconds(secondsBetweenWaves);

        currentWave++;

        if (currentWave < waves.Count - 1)
        {
            RuntimeManager.PlayOneShot(newWave);
        }

        GenerateWave();

        isProgressing = false;
    }
    void FixedUpdate()
    {
        // TODO: This is expensive. Optimize later.
        remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (isProgressing || currentWave >= waves.Count || player == null)
        {
            return;
        }

        if (secondsUntilNextSpawn > 0)
        {
            secondsUntilNextSpawn -= Time.fixedDeltaTime;
            return;
        }

        if (enemiesToSpawn.Count <= 0 && remainingEnemies <= 0)
        {
            StartCoroutine(WaitFornextWave());
            return;
        }

        SpawnEnemy();

        secondsUntilNextSpawn = spawnInterval;
    }

    private void SpawnEnemy()
    {
        if (enemiesToSpawn.Count <= 0)
        {
            return;
        }

        int random = UnityEngine.Random.Range(0, spawnPoints.Count);
        horizontalVariance = UnityEngine.Random.Range(-horizontalVariance, horizontalVariance);
        Transform randomPoint = spawnPoints[random];

        Vector3 randomPosition = new Vector3(horizontalVariance + randomPoint.position.x, randomPoint.position.y);


        Instantiate(enemiesToSpawn[0], randomPosition, Quaternion.identity);
        enemiesToSpawn.RemoveAt(0);
    }

    public void GenerateWave()
    {
        if (player != null)
        {
            GenerateEnemies();
        }
    }

    private void GenerateEnemies()
    {
        if (currentWave >= waves.Count)
        {
            Debug.Log($"Wave {currentWave} is larger than the list of waves. Ending.");
            return;
        }

        List<GameObject> generatedEnemies = new List<GameObject>();
        Wave wave = waves[currentWave];

        while (wave.waveBudget > 0)
        {

            List<Enemy> affordableEnemies = wave.possibleEnemies.FindAll(enemy => enemy.cost <= wave.waveBudget);

            if (affordableEnemies.Count == 0)
            {
                break;
            }

            int randomEnemyId = UnityEngine.Random.Range(0, affordableEnemies.Count);
            int randomEnemyCost = affordableEnemies[randomEnemyId].cost;

            if (wave.waveBudget - randomEnemyCost >= 0)
            {
                generatedEnemies.Add(affordableEnemies[randomEnemyId].enemyPrefab);
                wave.waveBudget -= randomEnemyCost;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }
    public List<Transform> GetAllSpawnPoints()
    {
        List<Transform> spawnPoints = new();

        foreach (Transform childTransform in this.transform)
        {
            Transform spawnPoint = childTransform.GetComponent<Transform>();
            spawnPoints.Add(spawnPoint);
        }

        return spawnPoints;
    }

    // Serializable attribute lets us add and change in editor. 
    [System.Serializable]
    public class Enemy
    {
        public GameObject enemyPrefab;
        public int cost;
    }

    [System.Serializable]
    public class Wave
    {
        public int waveBudget;
        public List<Enemy> possibleEnemies = new();
    }
}
