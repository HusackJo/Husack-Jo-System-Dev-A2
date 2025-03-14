using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    public TextMeshProUGUI itemCountText;
    public Image displayImage;

    public void SetDisplayImage(Sprite newDisplayImage, int itemCount)
    {
        if (newDisplayImage != null)
        {
            displayImage.sprite = newDisplayImage;
        }

        if (itemCount > 1)
        {
            itemCountText.gameObject.SetActive(true);
            itemCountText.text = itemCount.ToString();
        }
        else if (itemCount == 1)
        {
            itemCountText.gameObject.SetActive(false);
            itemCountText.text = itemCount.ToString();
        }
    }
}
