using UnityEngine;

public class FireShot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject shotPrefab;
    public Transform firePoint;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(shotPrefab, firePoint.position, firePoint.rotation);
        }
    }

}
