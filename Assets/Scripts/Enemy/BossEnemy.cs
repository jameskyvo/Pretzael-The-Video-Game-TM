using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public GameObject player;
    public GameObject enemyPrefab;
    public int spawnCooldown;
    public int numToSpawn;
    public Vector3 spawnOffset = new Vector3(16, 0f, 0f);

    private int verticalSpawnOffset;
    private Transform playerPos;
    private bool isSpawning = false;


    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform;

        if (isSpawning == false)
        {
            StartCoroutine(SpawnEnemies(numToSpawn));
        }
    }

    IEnumerator SpawnEnemies(int numToSpawn)
    {
        isSpawning = true;

        if (numToSpawn == 0 || playerPos == null)
        {
            yield return null;
        }

        if (isSpawning)
        {
            yield return null;
        }

        yield return new WaitForSeconds(spawnCooldown);

        int halfEnemies = numToSpawn / 2;
        int verticalVariance = 1;

        for (int i = 0; i < halfEnemies; i++)
        {
            verticalVariance--;
            Instantiate(enemyPrefab, playerPos.position + spawnOffset, Quaternion.identity);
            Instantiate(enemyPrefab, playerPos.position - spawnOffset, Quaternion.identity);
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(spawnCooldown);
        isSpawning = false;
    }
}
