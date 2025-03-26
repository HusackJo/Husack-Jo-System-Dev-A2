using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    ////
    //public Transform uiParent;
    //public GameObject inventoryPanelPrefab;
    public InventorySlotUI[] ItemSlots;
    //
    private PlayerInventory inventory;

    private void Awake()
    {
        inventory = FindObjectOfType<PlayerInventory>().GetComponent<PlayerInventory>();
    }

    private void OnEnable()
    {
        inventory.onInventoryChanged += UpdateUI;
    }


    private void OnDisable()
    {
        inventory.onInventoryChanged -= UpdateUI;
    }

    public void UpdateUI()
    {
        //clear 
        //foreach (GameObject slot in slots)
        //{
        //    Destroy(slot);
        //}
        //slots.Clear();

        int index = 0;
        //update slots
        foreach (KeyValuePair<ItemSO, int> entry in inventory.items)
        {
            ItemSlots[index].SetDisplayImage(entry.Key.itemImage, entry.Value);

            ////
            //GameObject newSlot = Instantiate(inventoryPanelPrefab, uiParent);
            ////
            //if (newSlot == null)
            //{
            //    Debug.Log("Inventory slot ref is null");
            //}
            //else
            //{
            //    //
            //    InventorySlotUI newSlotUI = newSlot.GetComponent<InventorySlotUI>();
            //    //
            //    if (newSlotUI == null)
            //    {
            //        Debug.Log("Inventory slot UI ref is null");
            //    }
            //    else
            //    {
            //        //put all the info in the slot
            //        newSlotUI.SetDisplayImage(entry.Key.itemImage, entry.Value);
            //        slots.Add(newSlot);
            //    }
            //}
            index++;
        }
    }

}
