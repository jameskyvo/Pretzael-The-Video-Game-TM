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
    private Rigidbody2D rb;
    private bool outOfBounds = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isFiring == false && outOfBounds == false)
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

    void LateUpdate() 
    {
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        rb = GetComponent<Rigidbody2D>();

        float screenWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        float objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;

        outOfBounds = transform.position.x > screenBounds.x || transform.position.y > screenBounds.y || transform.position.x < -screenBounds.x || transform.position.y < -screenBounds.y;
}
}
