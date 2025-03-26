using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class FleeingEnemyBehaviors : MonoBehaviour
{
    public float detectionRange, detectionFovAngle;
    //
    private NavMeshAgent navAgent;
    private PlayerMovement playerRef;
    private Damageable myDamageable;
    private Animator myAnimator;
    private Collider myCollider;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
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

    //called on update in Enemy_Fleeing_Idle, returns true when detecting player.
    public bool IdleStateBehaviors()
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
        if (navAgent != null)
        {
            navAgent.SetDestination(this.transform.position + playerRef.transform.position);
        }
    }

    // called in update in flee state
    public void RunAwayBehaviors()
    {
        navAgent.SetDestination(transform.position + (this.transform.position - playerRef.transform.position));
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
