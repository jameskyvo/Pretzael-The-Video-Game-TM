using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float moveSpeed = 5f;
    public GameObject shotPrefab;
    public Transform firePoint;
    public float shotDelaySeconds;
    public float recoilForce = 5f;
    public float recoilDecay = 10f;

    private Vector2 recoilVelocity;
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
            StartCoroutine(FireShot());
        }
    }

    private void FixedUpdate()
    {
        Vector2 moveVelocity = moveInput.normalized * moveSpeed;

        rb.linearVelocity = moveVelocity + recoilVelocity;
        recoilVelocity = Vector2.Lerp(
            recoilVelocity,
            Vector2.zero,
            recoilDecay * Time.fixedDeltaTime
        );
    }

    void LateUpdate() // Use LateUpdate to ensure all movement logic is finished
    {
        Vector3 currentPos = transform.position;

        // Clamp the position within calculated world-space bounds
        currentPos.x = Mathf.Clamp(currentPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        currentPos.y = Mathf.Clamp(currentPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);

        transform.position = currentPos;
    }

    IEnumerator FireShot()
    {
        if (!canFire)
        {
            yield break;
        }

        canFire = false;

        recoilVelocity += -(Vector2)transform.up * recoilForce;
        Instantiate(shotPrefab, firePoint.position, firePoint.rotation);

        yield return new WaitForSeconds(shotDelaySeconds);

        canFire = true;
    }
}
