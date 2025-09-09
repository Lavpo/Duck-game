using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAimedShot : MonoBehaviour
{
    [SerializeField] private GameObject gunstartlocation;
    [SerializeField] private SpriteRenderer sr;

    private float angle;

    public bool IsRotating;

    private void Update()
    {
        //shows mouse position on the screen
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //sets direction by finding a cordinates of a vector
        Vector2 direction = (mousePos - (Vector2)gunstartlocation.transform.position).normalized;

        //sets an angle using arctangens function 
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //creates rotation on a specific adjusted angle
        Quaternion rotation = Quaternion.AngleAxis(angle, transform.forward);

        //sets current object rotation to new created one
        gameObject.transform.rotation = rotation;

        Vector3 sh = gunstartlocation.transform.localScale;

        if (((angle > 90 && angle < 180) || (angle < -90 && angle > -180)) && IsRotating == true)
        {
            IsRotating = false; //will prevent infinite rotations

            //Flips a player on other direction
            sr.flipX = true;

            //Flips a gun on other direction
            sh = new Vector3(sh.x, sh.y * -1, sh.z);
            gunstartlocation.transform.localScale = sh;
        }
        else if (angle < 90 && angle > -90 && IsRotating == false)
        {
            IsRotating = true; //will prevent infinite rotations

            //Flips a player on other direction
            sr.flipX = false;

            //Flips a gun on other direction
            sh = new Vector3(sh.x, sh.y * -1, sh.z);
            gunstartlocation.transform.localScale = sh;
        }
    }
}
