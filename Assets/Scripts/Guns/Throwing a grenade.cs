using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;

public class Throwingagrenade : MonoBehaviour
{
    // private values
    float ydirection;
    float xdirection;
    float yCopy = 0;
    float changedangle;
    bool readyToThrow;

    // some stuff to initialize

    [Header("Angle and xThrowForce")]
    [SerializeField] float angle;
    [SerializeField] float throwForce; // value that is used to multiply x variable 

    [Header("Bullet GameObject")]
    [SerializeField] GameObject pref; // current bullet gameObject

    void Start()
    {
        // transforming current angle to rad and passing to tan function
        changedangle = Mathf.Tan(angle * Mathf.Deg2Rad);
        yCopy = ydirection;
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

            // sets down x and y directions, that will work 
            xdirection = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
            ydirection = changedangle * xdirection;




            // multiplier
            ydirection *= 100;
            xdirection *= 100;

            Debug.Log("xdirection = "+  xdirection + " :: ydirection = " + ydirection);

            // instantiate an object
            GameObject bulletpref = Instantiate(pref, gameObject.transform.position, gameObject.transform.rotation); 

            // takes a rigidbody from this given object    
            Rigidbody2D rb = bulletpref.GetComponent<Rigidbody2D>();

            // Adds force to the given object / prefab
            rb.AddForce(new Vector2(xdirection, ydirection)); 
            readyToThrow = false;
        }
    }

}
