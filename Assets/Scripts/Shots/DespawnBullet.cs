using UnityEngine;

public class DespawnBullet : MonoBehaviour
{
    public float despawnTimer = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, despawnTimer);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
