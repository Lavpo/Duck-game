using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(RaycastHit2D))]
public class Bulletscript : MonoBehaviour, IProjectileInitializer
{
    private Rigidbody2D rb;
    [SerializeField] private float destTime = 3f;
    [SerializeField] private LayerMask lm;

    // LineRenderer trajectoryline;
    // [SerializeField] 
    // int maxPoints = 50;
    // [SerializeField] 
    // float increment = 0.025f;

    // RaycastHit2D hit;
    private float speed, damage, angle;

    public void Initialise(float speed, float damage)
    {
        this.speed = speed;
        this.damage = damage;
    }

    // is used when I want to change initial values from other sctips.
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = speed * transform.right;
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right * -1, 1.5f, lm);
        Debug.DrawRay(transform.position, transform.right * -1.5f, Color.green, 2f);
        if (hit.collider != null)
        {
            Knockback kb = hit.collider.gameObject.GetComponent<Knockback>();

            if (kb != null)
            {
                kb.ApplyKnockback(gameObject.transform.position);
            }
            IDamageble idamageble = hit.collider.GetComponent<IDamageble>();
            if (idamageble != null) idamageble.Damage(damage);
            
            Destroy(gameObject);
        }
        Destroy(gameObject, destTime);
    }


    // void PredictTrajectory()
    // {
    //     rb = GetComponent<Rigidbody2D>();
    //     rb.velocity = gameObject.transform.right * (speed / mass);
    //     UnityEngine.Vector3 nextPosition;

    //     UpdateLineRender(maxPoints, (0, transform.position));
    // }

    // private void UpdateLineRender (int count, (int point, UnityEngine.Vector3 pos) pointPos)
    // {
    //     trajectoryline.positionCount = count;
    //     trajectoryline.SetPosition(pointPos.point, pointPos.pos);
    // }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if ((lm.value & (1 << collider.gameObject.layer)) > 0)
        {
            //Applying knockback to the enemy
            Knockback kb = collider.gameObject.GetComponent<Knockback>();

            if (kb != null)
            {
                kb.ApplyKnockback(gameObject.transform.position);
            }

            //Demaging the enemy
            IDamageble idamageble = collider.GetComponent<IDamageble>();
            if (idamageble != null) idamageble.Damage(damage);//IDE0031

            //Destroying bullet
            Destroy(gameObject);
        }
    }
}