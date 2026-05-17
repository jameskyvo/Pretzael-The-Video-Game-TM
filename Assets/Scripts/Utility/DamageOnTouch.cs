using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int collisionDamage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null)
        {
            return;
        }

        if (collision.gameObject.GetComponent<Health>() && collision.gameObject.tag != gameObject.tag)
        {
            Health targetHealth = collision.gameObject.GetComponent<Health>();

            targetHealth.TakeDamage(collisionDamage);
        }
    }
}
