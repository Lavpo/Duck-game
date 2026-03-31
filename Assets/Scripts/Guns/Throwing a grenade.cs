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
            // ScreenToViewportPoint is used here cuz normalised values are needed for the script to work
            // using ScreenToWorldPoint won't work, cuz it relies on POSITION of a character on a screen
            // (basically, the shooting angle wont only depent from the mouse position on a screen, but 
            // also from characters position on in the gamespace)

            // Value of x is subtracted by 0.5 to shift Viewport borders from (0, 1) to (-0.5, 0.5)
            xdirection = Camera.main.ScreenToViewportPoint(Input.mousePosition).x - 0.5f;
            ydirection = changedangle * xdirection;

            // multipliers
            xdirection *= 1000f;  
            ydirection *= 100f;

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
