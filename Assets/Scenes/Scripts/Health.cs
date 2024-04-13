using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    int health;

    [SerializeField] Image healthBar;

    private void Awake()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        this.health -= damage;
        if(this.health < 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
