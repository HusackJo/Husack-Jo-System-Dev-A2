using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FleeingEnemyStates
{
    IDLE,
    ALERT,
    FLEE,
}

public class FleeingEnemyBehaviors : MonoBehaviour
{
    public float detectionRange, detectionFovAngle;
    //
    private PlayerMovement playerRef;
    private Damageable myDamageable;
    private Animator myAnimator;
    private Collider myCollider;

    private void Awake()
    {
        myDamageable = GetComponent<Damageable>();
        myAnimator = GetComponent<Animator>();
        myDamageable.hasTakenDamage += TakeDamage;
    }

    private void Update()
    {
        if (playerRef == null)
        {
            FindObjectOfType<PlayerMovement>().TryGetComponent<PlayerMovement>(out playerRef);
        }
    }

    public void TakeDamage()
    {
        myAnimator.SetTrigger("Alerted");
    }

    //called on update in Enemy_Fleeing_Idle
    public bool IsAlerted()
    {
        Vector3 directionToPlayer = (playerRef.transform.position - transform.position).normalized;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionRange))
        {
            if (hit.transform == playerRef.transform)
            {
                Debug.Log($"Enemy: {name} detected player!");
                return true;
                //myAnimator.SetTrigger("Alerted");
            }
        } 
        return false;
    }

    //called after "Alerted" animation finishes
    public void GoToFlee()
    {
        if (myAnimator != null)
        {
            myAnimator.SetTrigger("AlertIsDone");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth healthRef = other.GetComponent<PlayerHealth>();
        if (healthRef != null)
        {
            healthRef.TakeDamage(200);
        }
        else
        {
            Debug.Log("Enemy Collision. No player ref attached to collision.");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //range circle
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // 2 lines denoting vision cone?
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + (detectionRange * transform.up));
        Gizmos.DrawLine(transform.position, transform.position + (detectionRange * transform.up));
    }
}
