using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{

    private Slider healthSlider;

    private void Awake()
    {
        healthSlider = GetComponentInChildren<Slider>();
    }

    public void UpdateHealthSlider(float currentHealthPercent)
    {
        if ( healthSlider != null )
        {
            healthSlider.value = currentHealthPercent;
        }
    }
}
