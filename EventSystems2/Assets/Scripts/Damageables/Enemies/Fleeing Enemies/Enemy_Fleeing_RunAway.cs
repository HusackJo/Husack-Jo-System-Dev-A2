using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Fleeing_RunAway : StateMachineBehaviour
{
    FleeingEnemyBehaviors behaviors;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        behaviors = animator.gameObject.GetComponent<FleeingEnemyBehaviors>();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        behaviors.RunAwayBehaviors();
    }
}
