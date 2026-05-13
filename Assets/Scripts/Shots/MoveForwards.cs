using JetBrains.Annotations;
using UnityEngine;

public class MoveForwards : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float shotSpeed = 10f;
    void Start()
    {
      Rigidbody2D rb = GetComponent<Rigidbody2D>();
      rb.linearVelocity = transform.up * shotSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
