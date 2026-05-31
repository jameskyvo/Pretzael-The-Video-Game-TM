using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class DespawnBullet : MonoBehaviour
{
    private float despawnTimer = 3f;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, despawnTimer);
    }

    void LateUpdate() 
    {
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        rb = GetComponent<Rigidbody2D>();

        float screenWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        float objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;

        if (transform.position.x > screenBounds.x || transform.position.y > screenBounds.y)
        {
            Destroy(gameObject, 0.05f);
        }

        if (transform.position.x < -screenBounds.x || transform.position.y < -screenBounds.y)
        {
            Destroy(gameObject, 0.05f);
        }
    }

}

