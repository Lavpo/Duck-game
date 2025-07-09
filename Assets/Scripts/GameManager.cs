using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager {  get; private set; }

    public HealthSystem playerHealth = new(100);
    void Awake()
    {
        Debug.Log("GameManager health: " + playerHealth.GetHealth());
        if(gameManager != null & gameManager != this)
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
        }
    }
}
