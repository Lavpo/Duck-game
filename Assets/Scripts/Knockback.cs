using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float knockbackTime = 0.2f;
    public float knockbackForce = 10f;

    public bool IsBeingKnockedBack { get; private set; }

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Call this when the enemy is hit, passing the bullet's position at collision.
    /// </summary>
    /// <param name="sourcePosition">Position of the bullet</param>
    public void ApplyKnockback(Vector2 sourcePosition)
    {
        if (IsBeingKnockedBack) return;

        // Compute direction from hit source → enemy
        Vector2 direction = (rb.position - sourcePosition).normalized; ;

        // Add a little upward bias (optional)

        direction.y = 0.2f;
        direction.x = Mathf.Sign(direction.x) * 2;

        direction.Normalize();

        Debug.DrawRay(rb.position, direction * 2f, Color.blue, 3f);
        Debug.DrawRay(rb.position, direction * 2f, Color.red, 1f);

        StartCoroutine(HandleKnockback(direction));
    }

    private IEnumerator HandleKnockback(Vector2 direction)
    {
        IsBeingKnockedBack = true;

        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockbackTime);

        IsBeingKnockedBack = false;
    }
}
