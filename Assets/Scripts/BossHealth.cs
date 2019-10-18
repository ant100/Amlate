using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private float bossHealth = 10.0f;
    [SerializeField] private PlayerAttack player = null;
    [SerializeField] private Text UIEnemyHealth = null;
    [SerializeField] private string winLevel = null;
    [SerializeField] private ShaderEffect ShaderEffect = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Punch"))
        {
            // check if attack was given
            if(player.isAttacking)
            {
                HurtBoss(player.PunchForce);
                StartCoroutine(ShaderEffect.BossShadeFX(0.1f));
            }
        }
    }

    private void HurtBoss(float hit)
    {
        bossHealth -= hit;
        UIEnemyHealth.text = "Enemy: " + bossHealth;
        if (bossHealth <= 0)
        {
            // boss is dead
            Debug.Log("you win");
            //SceneManager.LoadScene(winLevel);
        }
    }
}
