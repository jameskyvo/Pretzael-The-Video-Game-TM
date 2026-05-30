using System.Collections;
using UnityEngine;

public class FireShot : MonoBehaviour
{
    public GameObject shotPrefab;
    public Transform firePoint;
    public int coolDownSeconds;
    private bool canFire = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(SpawnProjectile());
        }
    }

    IEnumerator SpawnProjectile()
    {
        if (!canFire)
        {
            yield break;
        }

        canFire = false;

        Instantiate(shotPrefab, firePoint.position, firePoint.rotation);

        yield return new WaitForSeconds(coolDownSeconds);

        canFire = true;
    }
}
