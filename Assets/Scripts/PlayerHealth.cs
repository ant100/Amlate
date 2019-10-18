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
    [SerializeField] private ShaderEffect ShaderEffect = null;

    private void OnEnable()
    {
        UIHealth.text = "Health: " + playerHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            hurtPlayer(other.GetComponent<Hit>().Damage);
            StartCoroutine(ShaderEffect.PlayerShadeFX(0.1f));
        }
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
