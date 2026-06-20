using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;


public class Zombie_moving : MonoBehaviour, IDamageble
{
    public float enemySpeed;
    private Rigidbody2D rb;
    [SerializeField] private Transform player;
    [SerializeField] private float health;
    private SpriteRenderer sr;
    private Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (transform.position.x > player.position.x)
        {
            rb.linearVelocity = new Vector2(-enemySpeed, rb.linearVelocity.y);
            sr.flipX = false;
        }
        else
        {
            rb.linearVelocity = new Vector2(enemySpeed, rb.linearVelocity.y);
            sr.flipX = true;
        }

        animator.SetBool("IsWalking", Mathf.Abs(rb.linearVelocity.x) > 0.001f);
    }
    public void Damage(float takeDamage)
    {
        health -= takeDamage;
        Debug.Log("Enemy health: " + health);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    //void Flip()
    //{
    //    Vector2 scale = transform.localScale;
    //    scale.x *= -1;
    //    transform.localScale = scale;
    //}
}
