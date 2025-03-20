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
    public float detectionRange;
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

    public void GoToFlee()
    {
        if (myAnimator != null)
        {
            myAnimator.SetTrigger("AlertIsDone");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
