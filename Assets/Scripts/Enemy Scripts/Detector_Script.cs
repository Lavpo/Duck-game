using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Detector_Script : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject thing;
    private bool isCollided = false;

    void Start()
    {
        thing = GameObject.Find("Exclamation_Mark");
        spriteRenderer = thing.GetComponent<SpriteRenderer>(); //Gives an ability to use only sprite renderer things
    }
    private void Update()
    {
        if (isCollided == true)
        {
            spriteRenderer.color = new Color(1, 0, 0, 1); //Sets to red color
        }
        else
        {
            spriteRenderer.color = new Color(0, 0, 0, 0); //Sets to transperent color
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Detects the enemy
            isCollided = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Detects the enemy
            isCollided = false;
        }
    }
}