using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;


// If someone drags your script onto a GameObject without a Rigidbody2D, then your script will break.
// ^^^^^^^^^^^^^
[RequireComponent(typeof(Rigidbody2D))]
public class GrenadeBullet : MonoBehaviour, IProjectileInitializer
{
    private Rigidbody2D rb;
    [SerializeField] private float destTime = 3f;
    [SerializeField] private LayerMask lm;
    private PlayerAimedShot pis;
    private Knockback knockback;
    private float speed, damage;
    private float yacceleration = 1, xdirection = 1, angle;

    private Vector2 direction;

    // allows to change values of speed, damage and gravity through calling thing method
    public void Initialise(float speed, float damage, float angle)
    {
        this.speed = speed;
        this.damage = damage;
        this.angle = angle;

        Debug.Log(xdirection + " || " + yacceleration);  
    }
    public void InitialiseDirection(float directionx, float directiony)
    {
        xdirection = directionx;
        yacceleration = directiony;        
    }

    // is used when I want to change initial values from other sctips.
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, destTime);
        knockback = GetComponent<Knockback>();
        pis = GetComponent<PlayerAimedShot>();

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //sets direction by finding a cordinates of a vector
        direction = (mousePos - (Vector2)gameObject.transform.position).normalized;

        // provides force to the object
        // xdirection = yacceleration / Mathf.Tan(angle * Mathf.Deg2Rad);
        // ^^^^^^^^^ 
        // will change it later
    }

    private void FixedUpdate() {
        rb.AddForce(new Vector2(direction.x * speed, direction.y -= 9.8f * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if ((lm.value & (1 << collider.gameObject.layer)) > 0)
        {
            //Enemy Damaging
            knockback = collider.gameObject.GetComponent<Knockback>();

            if (knockback != null)
            {
                knockback.ApplyKnockback(transform.position);
            }

            IDamageble idamageble = collider.GetComponent<IDamageble>();
            if (idamageble != null) idamageble.Damage(damage);//IDE0031
            //Destroying bullet
            Destroy(gameObject);

        }
    }
}