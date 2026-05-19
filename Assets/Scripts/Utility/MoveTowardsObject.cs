using UnityEngine;

public class MoveTowardsObject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform otherTransform;
    public float speed = 3.0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (otherTransform == null)
        {
            return;
        }

        Vector2 movementDir = (otherTransform.position - transform.position).normalized;

        rb.linearVelocity = movementDir * speed;
    }
}
