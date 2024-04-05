using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : StateMachineBehaviour
{

    EnemyDamage enemyDamage;
    float time = 0;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyDamage = animator.GetComponent<EnemyDamage>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        time += 1 * Time.deltaTime;
        


        if (enemyDamage.inFightArea )
        {
            if (time > 3f)
            {
                animator.SetBool("Cover", false);
                animator.SetTrigger("Attack");
            }
        }
        else
        {
          animator.SetBool("Cover", false);
        }
        


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time = 0;
        
    }


    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
