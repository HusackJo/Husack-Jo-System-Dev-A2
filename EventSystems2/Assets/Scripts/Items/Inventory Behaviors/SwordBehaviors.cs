using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviors : MonoBehaviour
{
    //
    public LayerMask enemyLayer;
    public KeyCode attackButton;
    public GameObject colliderObject;
    public float swordDamage;
    //
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(attackButton))
        {
            animator.SetTrigger("SwingSword");
        }
    }

    public void EnableCollider()
    {
        colliderObject.SetActive(true);
    }
    public void DisableCollider()
    {
        colliderObject.SetActive(false);
    }
}
