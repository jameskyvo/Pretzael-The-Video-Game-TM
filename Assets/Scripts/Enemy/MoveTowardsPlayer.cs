using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    public float speed = 3.0f;
    private Transform playerTransform;
    private Rigidbody2D rb;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            return;
        }

        playerTransform = player.transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 movementDir = (playerTransform.position - transform.position).normalized;

        rb.linearVelocity = movementDir * speed;
    }
}

