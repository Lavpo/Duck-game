using System.Security.Cryptography;
using UnityEngine;

public class script : MonoBehaviour
{
    public GameObject Capsule;
    public GameObject Capsule2;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("--RESET BUTTON-- was presed");
            Capsule.transform.rotation = Quaternion.Euler(0, 0, 0);
            //Capsule.transform.Rotate(0, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("--ANGLE AXIS-- method was used");
            Quaternion rotation = Quaternion.AngleAxis(45, transform.right);
            gameObject.transform.rotation = rotation;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("--EULER-- method was used");
            Quaternion rotation = Quaternion.Euler(45f, 0f, 0);
            gameObject.transform.rotation = rotation;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("--ANGLE-- method was used");     
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("--FORWARD-- button was used");
            gameObject.transform.position = new Vector2(0, 0);
        }
        Capsule2.transform.rotation = Quaternion.Inverse(gameObject.transform.rotation);
    }
}
