using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int health;

    private SpriteRenderer spriteRenderer;
    private Color defaultColor;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            if (gameObject.tag == "Player")
            {
                GameOver();
            }
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health > 0)
        {
            StartCoroutine(FlickerRed());
        }
    }

    private IEnumerator FlickerRed()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.05f);

        spriteRenderer.color = defaultColor;
    }

    public void GameOver()
    {
        GameManager.instance.GameOver();
    }
}
