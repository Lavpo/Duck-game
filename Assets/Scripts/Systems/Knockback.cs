using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Timeline;

public class Knockback : MonoBehaviour
{
    public float knockbackTime = 0.2f;
    public float knockbackForce;
    private Rigidbody2D rb;
    private bool IsBeingKnockedBack;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && !IsBeingKnockedBack)
        {
            StartCoroutine(HandleKnockback());

            Debug.Log("Knockback test 1");
        }
    } 

    private IEnumerator HandleKnockback()
    {
        IsBeingKnockedBack = true;

        rb.AddForce(new Vector2(10, 1) * knockbackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockbackTime);

        IsBeingKnockedBack = false;

        Debug.Log("Knockback test 2");
    }
}
