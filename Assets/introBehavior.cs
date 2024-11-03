using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introBehavior : StateMachineBehaviour {

    private int rand;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        rand = Random.Range(0, 2); // Randomly choose between 0 and 1
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // Check if the animation is nearing its end
        if (stateInfo.normalizedTime >= 1.0f) // Animation has completed
        {
            // Transition to either idle or jump based on the random value
            if (rand == 0)
            {
                animator.SetTrigger("idle");
            }
            else
            {
                animator.SetTrigger("jump");
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // Any cleanup if necessary
    }
}