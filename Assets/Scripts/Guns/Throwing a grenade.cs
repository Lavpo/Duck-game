using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;

public class Throwingagrenade : MonoBehaviour
{
    [Header("Something to Initialize")]
    [SerializeField] GameObject pref;
    [SerializeField] float throwForce;

    [Header("X and Y variables")]
    [SerializeField] float ydirection = 100;
    float conydirection = 0;
    float xdirection;
    float angle = 45;

    bool readyToThrow;

    void Start()
    {
        conydirection = ydirection;
    }
    void Update()
    {
        // invokes ThrowObject script
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            readyToThrow = true;
        }
    }
    private void FixedUpdate() {
        if(readyToThrow)
        {
            xdirection = ydirection / Mathf.Tan(angle * Mathf.Deg2Rad);

            // instantiate an object
            GameObject bulletpref = Instantiate(pref, gameObject.transform.position, gameObject.transform.rotation); 

            // takes a rigid body from this given object
            Rigidbody2D rb = bulletpref.GetComponent<Rigidbody2D>();

            // Adds force to the given object / prefab
            rb.AddForce(new Vector2(xdirection, ydirection)); 
            readyToThrow = false;
        }
    }

}
