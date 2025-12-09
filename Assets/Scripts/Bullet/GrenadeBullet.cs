using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;


// If someone drags your script onto a GameObject without a Rigidbody2D, then your script will break.
// ^^^^^^^^^^^^^
[RequireComponent(typeof(Rigidbody2D))]
public class GrenadeBullet : MonoBehaviour
{
    [SerializeField] private float destTime = 3f;
    [SerializeField] private LayerMask lm;
    private Knockback knockback;
    private Rigidbody2D rb;
    private float speed = 10, damage;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, destTime);
    }
    private void FixedUpdate()
    {
        // adds gravity to the current object (not necessary)
        rb.AddForce(new Vector2(0, -9.8f));

        // adds rotation to the object based on an angle
        rb.rotation = Mathf.Atan2( rb.velocity.y, rb.velocity.x ) * Mathf.Rad2Deg;
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