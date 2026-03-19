using UnityEngine;
using UnityEngine.SceneManagement;

public class Spike_damage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            SceneManager.LoadScene("Scene");
        }
    }
}
