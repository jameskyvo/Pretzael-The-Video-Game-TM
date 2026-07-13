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
    private bool isHealing = false;

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
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
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

    internal void Heal(int healthAwarded)
    {
        if (isHealing == true)
        {
            return;
        }

        isHealing = true;

        health += healthAwarded;

        isHealing = false;
    }
}
