using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class HeadCoolZone : MonoBehaviour
{
    [SerializeField] private string detectionTag = "Player";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag))
        {
            Destroy(gameObject);
        }
    }
}
