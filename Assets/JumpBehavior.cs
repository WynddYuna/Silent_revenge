using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehavior : StateMachineBehaviour
{
    private float timer;
    public float minTime; // Minimum time before switching to idle
    public float maxTime; // Maximum time before switching to idle

    private Transform playerPos; // Reference to the player's position
    public float speed; // Speed at which the boss moves towards the player
    public float attackDistance; // Distance at which the boss will damage the player

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timer = Random.Range(minTime, maxTime); // Randomize the timer
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // Move towards the player
        Vector2 target = new Vector2(playerPos.position.x, animator.transform.position.y);
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed * Time.deltaTime);

        // Check distance to decide if it should deal damage
        if (Vector2.Distance(animator.transform.position, playerPos.position) < attackDistance)
        {
            // Call the attack method on the boss to deal damage
            Boss boss = animator.GetComponent<Boss>();
            if (boss != null && !boss.isDead)
            {
                // Assuming the Boss class has a method to deal damage
                boss.OnTriggerEnter2D(playerPos.GetComponent<Collider2D>());
            }
        }

        // Timer logic to switch to idle state
        if (timer <= 0)
        {
            animator.SetTrigger("idle"); // Switch to idle state
        }
        else 
        {
            timer -= Time.deltaTime; // Decrease timer
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // Any cleanup if necessary
    }
}