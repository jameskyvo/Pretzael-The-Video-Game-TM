using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static WaveSpawner;

public class WaveSpawner : MonoBehaviour
{
    public int currentWave;
    public Transform spawnLocation;
    public int waveDuration;
    public int secondsBetweenWaves;
    public List<Wave> waves = new List<Wave>();
    public List<GameObject> enemiesToSpawn = new List<GameObject>();
    public float spawnTimer;

    private float spawnInterval = 1;
    private bool isProgressing = false;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        GenerateWave();
    }

    IEnumerator WaitFornextWave()
    {
        isProgressing = true;

        yield return new WaitForSeconds(secondsBetweenWaves);

        currentWave++;

        GenerateWave();

        isProgressing = false;
    }
    void FixedUpdate()
    {
        if (isProgressing || currentWave >= waves.Count || player == null)
        {
            return;
        }
        if (spawnTimer <= 0 )
        {
            // if theres enemies left to spawn
            if (enemiesToSpawn.Count > 0)
            {
                Instantiate(enemiesToSpawn[0], spawnLocation.position, Quaternion.identity);
                enemiesToSpawn.RemoveAt(0);
                // reset spawn timer
                spawnTimer = spawnInterval;
            } else
            {
                // Wait for 5 seconds
                StartCoroutine(WaitFornextWave());
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
        }
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
            GameManager.instance.Victory();
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

            int randomEnemyId = UnityEngine.Random.Range(0, wave.possibleEnemies.Count);
            int randomEnemyCost = wave.possibleEnemies[randomEnemyId].cost;

            if (wave.waveBudget - randomEnemyCost >= 0)
            {
                generatedEnemies.Add(affordableEnemies[randomEnemyId].enemyPrefab);
                wave.waveBudget -= randomEnemyCost;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
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
