using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float knockbacktime = 0.2f;
    public float knockbackForce = 100f;
    public float constForce = 5f;

    public bool isBeingKnockedBacked { get; private set; }

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ApplyKnockback(Vector3 sourceposition)
    {
        if (isBeingKnockedBacked) return;

        Vector2 direction = (transform.position - sourceposition).normalized;

        StartCoroutine(HandleKnockback(direction));
    }

    private IEnumerator HandleKnockback(Vector3 direction)
    {
        isBeingKnockedBacked = true;
        rb.velocity = Vector3.zero;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Force); //I think problem is here 

        yield return new WaitForSeconds(knockbacktime);

        rb.velocity = Vector3.zero;
        isBeingKnockedBacked = false;
    }
}
