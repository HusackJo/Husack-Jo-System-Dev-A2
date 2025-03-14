using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupBehaviors : MonoBehaviour
{
    public ItemSO itemSO;
    public int itemQuantity;

    private void Start()
    {
        //poorly set prefabs don't break stacking limits
        if (itemQuantity > itemSO.stackLimit)
        {
            itemQuantity = itemSO.stackLimit;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            if (playerInventory.AddItemPickup(this))
            {
                Destroy(gameObject);
            }
        }
    }
}
