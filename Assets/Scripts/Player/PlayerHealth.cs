using UnityEngine;

public class PlayerHealth : Health
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Die()
    {
        Destroy(gameObject);
        GameManager.instance.GameOver();
    }
}
