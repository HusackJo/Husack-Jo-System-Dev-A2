using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotionBehaviors : MonoBehaviour
{
    public KeyCode useButton;
    public ItemSO healPotSORef;
    //
    private PlayerInventory inventory;
    private PlayerHealth health;

    private void Awake()
    {
        GameObject playerRef = FindObjectOfType<PlayerInventory>().gameObject;
        inventory = playerRef.GetComponent<PlayerInventory>();
        health = playerRef.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(useButton))
        {
            UsePotion();
        }
    }

    private void UsePotion()
    {
        health.HealPlayer(100);

        inventory.RemoveItem(healPotSORef, 1);
        Debug.Log($"using potion. {inventory.items[healPotSORef]} potions left");
    }
}
