using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemClass
{
    CONSUMABLE,
    WEAPON,
    CURRENCY,
}

[CreateAssetMenu (fileName = "ItemSO", menuName = "ScriptableObjects/ItemSO")]
public class ItemSO : ScriptableObject
{
    //UI
    public string itemName;
    public Sprite itemImage;
    //Func
    public GameObject itemPrefab;
    public ItemClass itemClass;
    public int stackLimit;
}
