using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float playerHealth = 10.0f;
    [SerializeField] private Text UIHealth = null;
    [SerializeField] private string gameOverLevel = null;

    private void OnEnable()
    {
        UIHealth.text = "Health: " + playerHealth;
    }

    public void hurtPlayer(float hit)
    {
        playerHealth -= hit;
        UIHealth.text = "Health: " + playerHealth;

        if(playerHealth <= 0)
        {
            // player is dead
            SceneManager.LoadScene(gameOverLevel);
        }
    }

}
