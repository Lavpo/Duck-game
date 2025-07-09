using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instatiate : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject gb;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Instantiate(gb, gameObject.transform.position, gameObject.transform.rotation);
        }
    }
}
