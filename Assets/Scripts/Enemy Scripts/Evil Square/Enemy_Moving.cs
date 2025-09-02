using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;


public class Enemy_Moving : MonoBehaviour, IDamageble
{
    public float enemySpeed;
    private Rigidbody2D rb;
    [SerializeField] private Transform player;
    [SerializeField] private float health;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (gameObject.transform.position.x > player.position.x)
        {
            rb.velocity = new Vector2(-enemySpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(enemySpeed, rb.velocity.y);
        }
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
