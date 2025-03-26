using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public delegate void HasTakenDamage();
    public event HasTakenDamage hasTakenDamage;
    //
    public float maxHealth;
    public ParticleSystem hitEffect;


    public void TakeDamage(float amount)
    {
        if (hitEffect != null)
        {
            hitEffect.Play();
        }

        maxHealth -= amount;
        if (maxHealth <= 0)
        {
            Destroy(gameObject);
            return;
        }
        hasTakenDamage?.Invoke();
    }
}
