using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float moveSpeed = 5f;
    public GameObject shotPrefab;
    public Transform firePoint;
    public float shotDelaySeconds;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    
    private bool canFire = true;
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        rb = GetComponent<Rigidbody2D>();

        // Prevents player from leaving the bounds halfway
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(SpawnProjectile());
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
    }

    void LateUpdate() // Use LateUpdate to ensure all movement logic is finished
    {
        Vector3 currentPos = transform.position;

        // Clamp the position within calculated world-space bounds
        currentPos.x = Mathf.Clamp(currentPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        currentPos.y = Mathf.Clamp(currentPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);

        transform.position = currentPos;
    }

    IEnumerator SpawnProjectile()
    {
        if (!canFire)
        {
            yield break;
        }

        canFire = false;

        Instantiate(shotPrefab, firePoint.position, firePoint.rotation);

        yield return new WaitForSeconds(shotDelaySeconds);

        canFire = true;
    }
}
