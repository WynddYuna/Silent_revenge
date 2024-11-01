using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject unlockUICanvas; // Reference to the unlock UI
    

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    bool isFacingRight = true;

    [Header("Movement")]
    public float moveSpeed = 5f;
    private float horizontalMovement; // Keep this private

    [Header("Jumping")]
    public float jumpPower = 10f;
    public int maxJumps = 2;
    private int jumpsRemaining;

    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;

    [Header("Gravity")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 2f;

    void Update()
    {
        // Update the player's velocity based on horizontal movement
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
        GroundCheck();
        Gravity();
        Flip();
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetFloat("magnitude", rb.velocity.magnitude);
    }

    private void Gravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (jumpsRemaining > 0)
        {
            if (context.performed)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                jumpsRemaining--;
                animator.SetTrigger("jump");
            }
            else if (context.canceled)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                jumpsRemaining--;
                animator.SetTrigger("jump");
            }
        }
    }

    private void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            jumpsRemaining = maxJumps;
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontalMovement < 0 || !isFacingRight && horizontalMovement > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }

    // New methods for button integration
    public void MoveLeft()
    {
        horizontalMovement = -1f; // Set to move left
    }

    public void MoveRight()
    {
        horizontalMovement = 1f; // Set to move right
    }

    public void StopMoving()
    {
        horizontalMovement = 0f; // Stop movement
    }

        private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player has entered the trigger area
        if (collision.CompareTag("UnlockTrigger"))
        {
            // Call a method to display the unlock UI
            ShowUnlockUI();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the player has exited the trigger area
        if (collision.CompareTag("UnlockTrigger"))
        {
            // Call a method to hide the unlock UI
            HideUnlockUI();
        }
    }

private void ShowUnlockUI()
{
    Debug.Log("Unlock UI Shown");
    unlockUICanvas.SetActive(true); // Show the unlock UI
}

private void HideUnlockUI()
{
    Debug.Log("Unlock UI Hidden");
    unlockUICanvas.SetActive(false); // Hide the unlock UI
}

}