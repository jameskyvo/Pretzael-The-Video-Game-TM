using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int health;
    public float damageCoolDownSeconds;

    private SpriteRenderer spriteRenderer;
    private Color defaultColor;
    private bool takingDamage = false;
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
        if (takingDamage == true)
        {
            return;
        }

        takingDamage = true;

        health -= damage;

        if (health > 0)
        {
            StartCoroutine(FlickerRed());
        }

    }

    private IEnumerator FlickerRed()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(damageCoolDownSeconds);

        spriteRenderer.color = defaultColor;
        takingDamage = false;

    }

    public void GameOver()
    {
        GameManager.instance.GameOver();
    }
}
