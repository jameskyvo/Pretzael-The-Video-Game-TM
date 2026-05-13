using UnityEngine;

public class MoveTowardsObject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform otherObject;
    public float speed = 3.0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (otherObject == null)
        {
            return;
        }

        Vector2 movementDir = (otherObject.position - transform.position).normalized;

        rb.linearVelocity = movementDir * speed;
    }
}
