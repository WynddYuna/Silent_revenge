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
    public float jumpForce; // Force applied to the jump
    private bool isJumping; // To check if the boss is currently jumping
    private Rigidbody2D rb; // Reference to the Rigidbody2D component

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timer = Random.Range(minTime, maxTime); // Randomize the timer
        rb = animator.GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        isJumping = false; // Reset jumping state
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // Move towards the player
        Vector2 target = new Vector2(playerPos.position.x, animator.transform.position.y);
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed * Time.deltaTime);

        // Check if the boss should jump
        if (!isJumping && Vector2.Distance(animator.transform.position, playerPos.position) < attackDistance)
        {
            // Initiate jump
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Set the vertical velocity for jumping
            isJumping = true; // Set jumping state
        }

        // Check distance to decide if it should deal damage
        if (Vector2.Distance(animator.transform.position, playerPos.position) < attackDistance)
        {
            // Call the attack method on the boss to deal damage
            Boss boss = animator.GetComponent<Boss>();
            if (boss != null && !boss.isDead)
            {
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
        // Reset jumping state on exit
        isJumping = false;
    }
}