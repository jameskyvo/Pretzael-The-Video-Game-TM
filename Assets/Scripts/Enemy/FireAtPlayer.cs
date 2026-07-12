using System.Collections;
using UnityEngine;

public class FireAtPlayer : MonoBehaviour
{
    public GameObject shotPrefab;
    public Transform[] firePoints;
    public int secondsBetweenBurstFire;
    public int attackCooldown;

    private bool isFiring = false;
    private int shotAmount = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isFiring == false)
        {
            StartCoroutine(Shoot());
        }
    }


    IEnumerator Shoot()
    {
        isFiring = true;

        for (int i = 0; i < shotAmount; i++)
        {
            foreach(Transform firePoint in firePoints)
            {
                Instantiate(shotPrefab, firePoint.position, firePoint.rotation);
            }
            yield return new WaitForSeconds(secondsBetweenBurstFire);
        }

        yield return new WaitForSeconds(attackCooldown);
        isFiring = false;
    }
}
