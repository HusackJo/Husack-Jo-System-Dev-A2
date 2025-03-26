using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;


public class FleeingEnemyBehaviors : MonoBehaviour
{
    public float detectionRange, detectionFovAngle;
    //
    private GameManager gameManager;
    private NavMeshAgent navAgent;
    private PlayerMovement playerRef;
    private Damageable myDamageable;
    private Animator myAnimator;
    private Collider myCollider;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        gameManager.spawnPlayer += GetPlayerRef; 
        navAgent = GetComponent<NavMeshAgent>();
        myDamageable = GetComponent<Damageable>();
        myAnimator = GetComponent<Animator>();
        myDamageable.hasTakenDamage += TakeDamage;
    }

    private void GetPlayerRef()
    {
        playerRef = FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>();
    }

    public void TakeDamage()
    {
        myAnimator.SetTrigger("Alerted");
    }

    //called on update in Enemy_Fleeing_Idle, returns true when detecting player.
    public bool IdleStateBehaviors()
    {
        if (playerRef != null)
        {
            //angletoplayer = vector3.angle(forward, directiontoplayer)
            Vector3 directionToPlayer = (playerRef.transform.position - transform.position).normalized;
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            if (angleToPlayer < transform.rotation.z + detectionFovAngle/2)
            {
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
            }
        }
        return false;
    }

    public void IdleStateRotations()
    {
        this.transform.parent.Rotate(new Vector3(0, 0, UnityEngine.Random.Range(-90, 90)));
        
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
        navAgent.updateRotation = true;
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
        float halfFOV = detectionFovAngle / 2;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.forward );
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.forward );
        Vector3 leftRayDirection = leftRayRotation * transform.up ;
        Vector3 rightRayDirection = rightRayRotation * transform.up ;
        Gizmos.DrawRay(transform.position, leftRayDirection * detectionRange);
        Gizmos.DrawRay(transform.position, rightRayDirection * detectionRange);
    }
}

