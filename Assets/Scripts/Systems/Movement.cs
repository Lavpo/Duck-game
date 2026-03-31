using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 1f;
    private Vector2 block;
    private Vector2 something;
    void Start()
    {
        Application.targetFrameRate = 1000;
        something = new Vector2 (10, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    { 
        Movementt();
    }
    void Movementt()
    {
        if (block.x <= something.x)
        {
            block = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            transform.position = block;
        }
    }
}
