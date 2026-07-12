using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    public float maxSpeed;
    public float minSpeed;
    public float speed;
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

        speed = Random.Range(minSpeed, maxSpeed);

        playerTransform = player.transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (player == null)
        {
            return;
        }

        Vector2 movementDir = (playerTransform.position - transform.position).normalized;

        rb.linearVelocity = movementDir * speed;
    }
}

