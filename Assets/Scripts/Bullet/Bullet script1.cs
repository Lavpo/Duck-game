using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class Bulletscript : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float destTime = 3f;
    [SerializeField] private LayerMask lm;
    private float speed;
    private float damage;

    public void Initialise(float speed, float damage = 0)
    {
        this.speed = speed;
        this.damage = damage;
    }

    // is used when I want to change initial values from other sctips.
    private void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = speed * transform.right; 
        Destroy(gameObject, destTime);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if ((lm.value & (1 << collider.gameObject.layer)) > 0)
        {
            //Applying knockback to the enemy
            Knockback kb = collider.gameObject.GetComponent<Knockback>();

            if (kb != null)
            {
                kb.ApplyKnockback(transform.position);
            }

            //Demaging the enemy
            IDamageble idamageble = collider.GetComponent<IDamageble>();
            if (idamageble != null) idamageble.Damage(damage);//IDE0031

            //Destroying bullet
            Destroy(gameObject);
           
        }
        Debug.Log("Layer has been touched, but bullet hasn't destroyed");
    }
}