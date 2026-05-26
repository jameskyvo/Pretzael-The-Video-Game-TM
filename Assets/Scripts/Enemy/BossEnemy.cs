using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public GameObject player;
    public GameObject enemyPrefab;
    public int spawnCooldown;
    public int numToSpawn;
    public Vector3 spawnOffset = new Vector3(16, 0f, 0f);
    public int attackCooldown;
    public int chargeSpeed;
    public GameObject shotPrefab;
    public Transform firePoint;
    public int shotAmount = 3;

    private Rigidbody2D rb;
    private int verticalSpawnOffset;
    private Transform playerTransform;
    private bool isCharging = false;
    private bool isSpawning = false;
    private bool isFiring = false;
    private float secondsBetweenBurstFire = 0.2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        playerTransform = player.transform;
        if (isSpawning == false)
        {
            StartCoroutine(SpawnEnemies(numToSpawn));
        }

        if (isCharging == false)
        {
            StartCoroutine(Charge());
        }

        if (isFiring == false)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator SpawnEnemies(int numToSpawn)
    {
        isSpawning = true;

        if (numToSpawn == 0 || playerTransform == null)
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
            Instantiate(enemyPrefab, playerTransform.position + spawnOffset, Quaternion.identity);
            Instantiate(enemyPrefab, playerTransform.position - spawnOffset, Quaternion.identity);
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(spawnCooldown);
        isSpawning = false;
    }

    IEnumerator Charge()
    {
        isCharging = true;

        if (isCharging)
        {
            yield return null;
        }
        // Shake camera

        // Play charge sound

        // store current position
        Vector3 originalPosition = transform.position;
        // Get direction to charge
        Vector2 movementDir = GetDirectionToPlayer(originalPosition);
        // move the distance
        rb.linearVelocity = movementDir * chargeSpeed;
        // wait x seconds
        yield return new WaitForSeconds(3f);
        // return to original point
        while (Vector2.Distance(transform.position, originalPosition) > 0.1f)
        {
            Vector2 returnDir =
                (originalPosition - transform.position).normalized;

            rb.linearVelocity = returnDir * chargeSpeed;

            yield return null;
        }

        rb.linearVelocity = Vector2.zero;
        transform.position = originalPosition;

        isCharging = false;


    }

    private Vector2 GetDirectionToPlayer(Vector3 originalPosition)
    {
        return (player.transform.position - originalPosition).normalized;
    }

    IEnumerator Shoot()
    {
        isFiring = true;

        if (isFiring)
        {
            yield return null;
        }

        for (int i = 0; i < shotAmount; i++)
        {
            Instantiate(shotPrefab, firePoint.position, firePoint.rotation);
            yield return new WaitForSeconds(secondsBetweenBurstFire);
        }

        yield return new WaitForSeconds(attackCooldown);
        isFiring = false;
    }
}
