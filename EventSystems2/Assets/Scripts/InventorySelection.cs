using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class InventorySelection : MonoBehaviour
{
    public KeyCode swapButton, swapButton2;
    public Transform equippedItemParent;
    //
    private int selectedInventorySlot;
    private PlayerInventory inventory;
    private GameObject currentSelectedItem;
    private void Awake()
    {
        inventory = GetComponent<PlayerInventory>();
        selectedInventorySlot = 0;
    }
    void Update()
    {
        if (Input.GetKeyDown(swapButton) || Input.GetKeyDown(swapButton2))
        {
            Debug.Log("button press");
            //swaps slots - i dont wanna do a proper system for this
            switch (selectedInventorySlot)
            {
                case 0:
                    SwapSelectedSlot(inventory.items.ElementAt(0).Key);
                    selectedInventorySlot = 1;
                    break;
                case 1:
                    SwapSelectedSlot(inventory.items.ElementAt(1).Key);
                    selectedInventorySlot = 2;
                    break;
                case 2:
                    SwapSelectedSlot(inventory.items.ElementAt(0).Key);
                    selectedInventorySlot = 1;
                    break;
            }
        }
    }

    public void SwapSelectedSlot(ItemSO newSelectedItem)
    {
        //destroy then instantiate
        Destroy(currentSelectedItem);
        currentSelectedItem = Instantiate(newSelectedItem.itemPrefab, equippedItemParent);
    }
}
