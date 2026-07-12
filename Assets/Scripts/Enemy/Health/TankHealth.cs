using UnityEngine;

public class TankHealth : Health
{
    public GameObject objectToSpawn;
    public override void Die()
    {
        Instantiate(objectToSpawn, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }
}
    
