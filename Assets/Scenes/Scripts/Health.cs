using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health;

    void TakeDamage(int damage)
    {
        this.health -= damage;
    }
}
