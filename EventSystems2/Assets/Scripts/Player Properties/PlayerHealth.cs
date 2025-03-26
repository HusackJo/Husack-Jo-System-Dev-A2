using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerUI playerUI;
    public float maxHealth;
    public float drainRate, drainScaling;
    //
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUIToCurrent();
    }
    private void FixedUpdate()
    {
        TakeDamage(drainRate);
        drainRate += drainScaling;
        UpdateHealthUIToCurrent();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            //then you die
        }
    }

    public void UpdateHealthUIToCurrent()
    {
        playerUI.UpdateHealthSlider(currentHealth / maxHealth);
    }

    public void HealPlayer(float percentHealing)
    {
        currentHealth += maxHealth / (percentHealing / 100);
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
