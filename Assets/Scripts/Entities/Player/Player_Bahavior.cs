using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bahavior : MonoBehaviour
{
    [SerializeField] private HealthScript healthScript;
    private void Start()
    {
       healthScript = GetComponent<HealthScript>();
       healthScript.SetMaxHealth(100);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            PlayerTakeDamage(20);
            Debug.Log(GameManager.gameManager.playerHealth.GetHealth());
            healthScript.SetHealth(GameManager.gameManager.playerHealth.GetHealth());
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerTakeHeal(20);
            Debug.Log(GameManager.gameManager.playerHealth.GetHealth());
            healthScript.SetHealth(GameManager.gameManager.playerHealth.GetHealth());
        }
    }
    private void PlayerTakeDamage (int dmg)
    {
        GameManager.gameManager.playerHealth.DamageUnit(dmg);
    }
    private void PlayerTakeHeal(int heal)
    {
        GameManager.gameManager.playerHealth.HealUnit(heal);
    }
}
