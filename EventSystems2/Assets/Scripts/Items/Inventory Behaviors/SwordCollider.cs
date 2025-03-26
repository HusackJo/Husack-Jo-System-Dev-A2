using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollider : MonoBehaviour
{
    SwordBehaviors sword;

    private void Awake()
    {
        sword = GetComponentInParent<SwordBehaviors>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Damageable"))
        {
            Damageable damageable = other.gameObject.GetComponent<Damageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(sword.swordDamage);
            }
            //else if (other.gameObject.GetComponentInParent<Damageable>() != null)
            //{
            //    //print("aaa");
            //    other.gameObject.GetComponentInParent<Damageable>().TakeDamage(sword.swordDamage);
            //}
        }
    }
}
