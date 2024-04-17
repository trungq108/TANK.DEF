using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] Image healthBar;
    int health;

    [SerializeField] bool isPlayer;

    private void Awake()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        health = maxHealth;
        healthBar.fillAmount = (float)health / maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.fillAmount = (float)health / maxHealth;

        if (this.health <= 0)
        {
            gameObject.SetActive(false);
            if(isPlayer) { GameManager.instance.GameOver(); }
            else 
            {                
                GameManager.instance.AddScore(1);
            }
            VFXpool.instance.PlayVFX(this.transform.position);
        }
    }
}
