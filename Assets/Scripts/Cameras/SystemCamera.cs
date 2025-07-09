using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemCamera : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject maincam;
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        maincam = GameObject.Find("Main Camera");
    }
    
    // Update is called once per frame
    void LateUpdate()
    {

        // CODE THAT ALOWS TO SET TO X AND Y BY A PLAYER POSITION AND PLACE LIMITS FOR VIEW
        //useful if you like to hide something from a player :)

        float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(player.transform.position.y, yMin, yMax);

        // CODE THAT ALOWS TO SET X AND Y BY A PLAYER POSITION
        /*float yy = player.transform.position.y;
        float xx = player.transform.position.x;*/

        // CODE THAT ALOWS TO SET THE CAMERA BY A PLAYER POSITION(in this case main camera is a part of gameObject) 
        //In this case we should use z because camera works ONLY in 3d direction (sadly).   
        maincam.transform.position = new Vector3(x, y, gameObject.transform.position.z);
    }
}
