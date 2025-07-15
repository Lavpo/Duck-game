using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.UIElements;

public class Bullet_hell_pattern : MonoBehaviour
{
    private static int rotation_points = 7;
    private int radius = 1;
    private float angle;
    private Vector2[] rotation_positions = new Vector2[rotation_points];
    private float rotation_speed = 100;
    [SerializeField] private float bullet_speed;

    [SerializeField] private GameObject bulletpattern;

    // Update is called once per frame
    void Update()
    {
        //need to get a postion for each point firstly, and than provide a logic 
        SunScript sun = GetComponent<SunScript>();
        sun.Speed(bullet_speed);
        angle = 2 * Mathf.PI / rotation_points;

        for (int i = 0;  i < rotation_points; i++)
        {
            float step = angle * i;
            float x = gameObject.transform.position.x + radius * Mathf.Cos(step);
            float y = gameObject.transform.position.y + radius * Mathf.Sin(step);

            rotation_positions[i] = new Vector2(x, y);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            for (int i = 0; i < rotation_points; i++)
            {
                Instantiate(bulletpattern, rotation_positions[i], Quaternion.Euler(0, 0, angle * i * Mathf.Rad2Deg));
            }
        }
    }
}
