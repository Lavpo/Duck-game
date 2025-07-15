using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunScript : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float destTime = 3f;
    [SerializeField] private LayerMask lm;
    private float damage;
    private float speed;
    public void Initialise(float speed, float damage = 0)
    {
        this.speed = speed;
        this.damage = damage;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = speed * transform.right;

        Destroy(gameObject, destTime);
    }
    public void Speed(float speedmulti)
    {
        speed *= speedmulti;
        if (speedmulti == 0) Debug.Log("Multiplier can't equal 0");
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if ((lm.value & (1 << collider.gameObject.layer)) > 0)
        {
            //Enemy Damaging
            Knockback kb = collider.gameObject.GetComponent<Knockback>();

            if (kb != null)
            {
                kb.ApplyKnockback(transform.position);
            }

            IDamageble idamageble = collider.GetComponent<IDamageble>();
            if (idamageble != null) idamageble.Damage(damage);//IDE0031
            //Destroying bullet
            Destroy(gameObject);

        }
    }
}
