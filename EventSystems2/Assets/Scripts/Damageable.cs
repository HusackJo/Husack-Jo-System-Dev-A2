using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public float maxHealth;

    public void TakeDamage(float amount)
    {
        maxHealth -= amount;
        if (maxHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
