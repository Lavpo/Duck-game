using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int direction;
    private Transform rb;
    [SerializeField] private LayerMask LayerToBeTouched;
    void Start()
    {
        rb = GetComponent<Transform>();
    }

    void Update()
    {
        PlatformMove();
    }
    void PlatformMove()
    {
        rb.position = new Vector2(rb.position.x + Time.deltaTime * MathF.Sign(direction), rb.position.y);
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.right * Mathf.Sign(direction), 0.55f, LayerToBeTouched);
        Debug.DrawRay(rb.position, Vector2.right * Mathf.Sign(direction) * 0.55f, Color.red);
        if (hit.collider != null)
        {
            direction *= -1;
        }
        if (direction == 0)
        {
            Debug.Log("Direction = 0");
        }
    }
}
