using UnityEngine;

public class DetectHit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int shotDamage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Health enemyHealth = collision.gameObject.GetComponent<Health>();

            if (enemyHealth != null) {
                enemyHealth.TakeDamage(shotDamage);
            }

            Destroy(gameObject);
        }
    }
}
