using System.Collections;
using UnityEngine;

public class ChargeAtPlayer : MonoBehaviour
{
    private bool isCharging = false;
    private GameObject player;
    private Rigidbody2D rb;
    public int chargeSpeed;
    public float windUpInSeconds;
    private int shakeDampening = 4;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isCharging == false)
        {
            StartCoroutine(Charge());
        }
    }

    IEnumerator Charge()
    {
        if (player == null)
        {
            yield break;
        }

        isCharging = true;

        // Shake body
        StartCoroutine(ShakeEnemy());
        // Play charge sound

        // store current position
        Vector3 originalPosition = transform.position;
        // Get direction to charge
        Vector2 movementDir = GetDirectionToPlayer(originalPosition);
        // move the distance
        rb.linearVelocity = movementDir * chargeSpeed;
        // wait x seconds
        yield return new WaitForSeconds(4f);

        rb.linearVelocity = Vector2.zero;

        isCharging = false;


    }

    private Vector2 GetDirectionToPlayer(Vector3 originalPosition)
    {
        return (player.transform.position - originalPosition).normalized;
    }

    IEnumerator ShakeEnemy()
    {
        Vector2 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < windUpInSeconds)
        {
            elapsedTime += Time.deltaTime;
            transform.position = startPosition + (Random.insideUnitCircle / shakeDampening);
            yield return null;
        }

        transform.position = startPosition;
    }
}
