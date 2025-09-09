using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Heallthscript : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float health;

    [SerializeField] private HealthScript healthScript;
    private void Start()
    {
        health = maxHealth;
        healthScript.SetMaxHealth(health);
    }
    public float TakeDamage(float damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }
        if (health == 0)
        {
            SceneManager.LoadScene("Scene");
        }
        healthScript.SetHealth(health);
        return health;
    }
    public float TakeHeal(float heal)
    {
        health += heal;
        if (health < 0)
        {
            health = 0;
        }
        if (health > 0)
        {
            health = maxHealth;
        }
        return health;
    }
}
