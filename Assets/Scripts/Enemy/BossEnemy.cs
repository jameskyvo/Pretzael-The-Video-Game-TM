using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public GameObject player;
    public GameObject enemyPrefab;
    public int spawnCooldown;
    public Vector3 spawnOffset = new Vector3(8f, 0f, 0f);

    private Transform playerPos;
    private bool isSpawning = false;


    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform;

        if (isSpawning == false)
        {
            StartCoroutine(SpawnEnemies(spawnCooldown));
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

        for (int i = 0; i < halfEnemies; i++)
        {
            Instantiate(enemyPrefab, playerPos.position + spawnOffset, Quaternion.identity);
            Instantiate(enemyPrefab, playerPos.position - spawnOffset, Quaternion.identity);
        }

        isSpawning = false;
    }
}
