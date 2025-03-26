using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Dictionary<ItemSO, int> items = new Dictionary<ItemSO, int>();
    public int maxSlots = 2;

    public delegate void OnInventoryChanged();
    public event OnInventoryChanged onInventoryChanged;
    //
    public InventoryUI inventoryUI { get; private set; }

    //private void Awake()
    //{
    //    inventoryUI = FindAnyObjectByType<InventoryUI>().GetComponent<InventoryUI>();
    //}

    private void Update()
    {
        if (inventoryUI == null)
        {
            inventoryUI = FindAnyObjectByType<InventoryUI>().GetComponent<InventoryUI>();
        }
    }
    /// <summary>
    /// Intended for use in debug. Adds without possible subtraction from an Item Pickup.
    /// </summary>
    /// <param name="newItem"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool AddItem(ItemSO newItem, int amount)
    {
        if (items.ContainsKey(newItem))
        {
            if (items[newItem] + amount >= newItem.stackLimit)
            {
                Debug.Log("Max stacks for this item type reached!");
                //if inventory isn't completley full and pickup can fill, pickup the difference
                if (items[newItem] < newItem.stackLimit)
                {
                    int countToFillMax = (newItem.stackLimit - items[newItem]);
                    items[newItem] += amount - countToFillMax;
                    Debug.Log($"Filled {newItem.itemName} slot to full ({countToFillMax} quantity added)");
                    onInventoryChanged?.Invoke();
                }
                return false;
            }
            items[newItem] += amount;
        }
        else
        {
            if (items.Count >= maxSlots)
            {
                Debug.Log("Inventory full!");
                
                return false;
            }
            items.Add(newItem, amount);
        }
        onInventoryChanged?.Invoke();
        return true;
    }

    /// <summary>
    /// Intended for use with Item Pickups.
    /// </summary>
    /// <param name="pickup"></param>
    /// <returns></returns>
    public bool AddItemPickup(ItemPickupBehaviors pickup)
    {
        ItemSO newItem = pickup.itemSO;
        int amount = pickup.itemQuantity;

        if (items.ContainsKey(newItem))
        {
            if (items[newItem] + amount >= newItem.stackLimit)
            {
                Debug.Log("Max stacks for this item type reached!");
                //if inventory isn't completley full and pickup can fill, pickup the difference
                if (items[newItem] < newItem.stackLimit)
                {
                    int countToFillMax = (newItem.stackLimit - items[newItem]);
                    items[newItem] += amount - countToFillMax;
                    pickup.itemQuantity -= countToFillMax;
                    Debug.Log($"Filled {newItem.itemName} slot to full ({countToFillMax} quantity added)");
                    onInventoryChanged?.Invoke();
                }
                return false;
            }
            items[newItem] += amount;
        }
        else
        {
            if (items.Count >= maxSlots)
            {
                Debug.Log("Inventory full! Item to pickup needs extra slot");
                
                return false;
            }
            items.Add(newItem, amount);
        }
        onInventoryChanged?.Invoke();
        return true;
    }

    public void RemoveItem(ItemSO itemToRemove, int amount)
    {
        if (items.ContainsKey(itemToRemove))
        {
            items[itemToRemove] -= amount;
            if (items[itemToRemove] <= 0)
            {
                items.Remove(itemToRemove);
            }
            onInventoryChanged?.Invoke();
        }
    }

    public bool HasItem(ItemSO item, int amount)
    {
        return items.ContainsKey(item) && items[item] >= amount;
    }

    public ItemSO GetItemSOFromSlot(int slot)
    {
        if (slot < 1 || slot > maxSlots)
        {
            Debug.Log("Slot value for item swap invalid");
            return null;
        }
        return items.ElementAt(slot).Key;
    }
}
