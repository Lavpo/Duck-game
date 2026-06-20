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
public class GrenadeBullet : MonoBehaviour, IProjectileInitializer
{
    [SerializeField] private float destTime = 3f;
    [SerializeField] private LayerMask lm;
    private Knockback knockback;
    private Rigidbody2D rb;
    private float damage, speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = speed * transform.right;
        Destroy(gameObject, destTime);
    }
    public void Initialise(float speed, float damage)
    {
        this.speed = speed;
        this.damage = damage;
    }


    private void FixedUpdate()
    {
        // adds gravity to the current object (not necessary)
        rb.AddForce(new Vector2(0, -9.8f));

        // adds rotation to the object based on an angle
        rb.rotation = Mathf.Atan2( rb.linearVelocity.y, rb.linearVelocity.x ) * Mathf.Rad2Deg;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if ((lm.value & (1 << collider.gameObject.layer)) > 0)
        {
            //Enemy Damaging
            knockback = collider.gameObject.GetComponent<Knockback>();

            IDamageble idamageble = collider.GetComponent<IDamageble>();
            if (idamageble != null) idamageble.Damage(damage);//IDE0031
            //Destroying bullet
            Destroy(gameObject);

        }
    }
}