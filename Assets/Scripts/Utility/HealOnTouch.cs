using UnityEngine;

public class HealOnTouch : MonoBehaviour
{
    public int healthAwarded;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null)
        {
            return;
        }

        if (collision.gameObject.tag == "Player")
        {
            Health targetHealth = collision.gameObject.GetComponent<Health>();

            targetHealth.Heal(healthAwarded);

            Destroy(gameObject);
        }
    }
}
