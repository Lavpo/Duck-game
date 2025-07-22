using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float knockbacktime = 0.2f;
    public float knockbackForce;
    public float constForce = 5f;

    public bool IsBeingKnockedBacked { get; private set; }

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ApplyKnockback(Vector3 sourceposition)
    {
        if (IsBeingKnockedBacked) return;

        Vector2 direction = (transform.position - sourceposition).normalized;
        //.normalized prevents an object to move with a higher speed when moving "diagonaly"

        if (direction.y < 0)
        {
            direction.y = 0;
        }

        StartCoroutine(HandleKnockback(direction));
    }

    private IEnumerator HandleKnockback(Vector3 direction)
    {
        IsBeingKnockedBacked = true;
        rb.velocity = Vector3.zero;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse); //I think problem is here 
        yield return new WaitForSeconds(knockbacktime);

        rb.velocity = Vector3.zero;
        IsBeingKnockedBacked = false;
    }
}
