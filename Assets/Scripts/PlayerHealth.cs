using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private float playerHealth = 10.0f;
    [SerializeField]
    private Text UIHealth;
    [SerializeField]
    private string gameOverLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hurtPlayer(float hit)
    {
        playerHealth -= hit;
        UIHealth.text = "Health: " + playerHealth;

        if(playerHealth <= 0)
        {
            // player is death
            SceneManager.LoadScene(gameOverLevel);
        }
    }

}
