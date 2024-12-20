using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public int health = 100; // Player health
    public Rigidbody2D rb; // Player rigidbody
    public Animator animator; // Player animator
    public GameObject unlockUICanvas;
    
    
    public float KBCounter;
    public float KBForce;
     public float KBTotalTime;
    public bool KnockFromRight;
     // Reference to the unlock UI

    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        animator = GetComponent<Animator>();
    }

    bool isFacingRight = true;
    private bool canMove = true; // New flag to control movement

    [Header("Movement")]
    public float moveSpeed = 5f; // Player movement speed
    private float horizontalMovement; // Keep this private

    [Header("Dashing")]
    public float dashSpeed = 1000f; // Dash speed
    public float dashDuration = 1f; // Dash duration
    public float dashCooldown = 0.1f; // Dash cooldown

    bool isDashing;
    bool canDash = true;

    TrailRenderer trailRenderer;

    [Header("Jumping")]
    public float jumpPower = 10f; // Jump power
    public int maxJumps = 2; // Maximum jumps
    private int jumpsRemaining;

    [Header("GroundCheck")]
    public Transform groundCheckPos; // Ground check position
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f); // Ground check size
    public LayerMask groundLayer; // Ground layer

    [Header("Gravity")]
    public float baseGravity = 2f; // Base gravity
    public float maxFallSpeed = 18f; // Maximum fall speed
    public float fallSpeedMultiplier = 2f; // Fall speed multiplier

    void Update()
    {
        if (!canMove) return; // Stop all movement and actions if canMove is false

        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetFloat("magnitude", Mathf.Abs(horizontalMovement)); // Use absolute value for magnitude

        if (isDashing)
        {
            return;
        }
  if(KBCounter <= 0){

             rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
        }else{

            if(KnockFromRight==true){
                rb.velocity=new Vector2(-KBForce,KBForce);
    
            }
            if(KnockFromRight==false){
                rb.velocity=new Vector2(KBForce,KBForce);

            }
            KBCounter -= Time.deltaTime;
        }
        // Update the player's velocity based on horizontal movement
        
        GroundCheck();
        Gravity();
        Flip();
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
        if (!canMove) return; // Stop move input if canMove is false

        horizontalMovement = context.ReadValue<Vector2>().x;

        // If there is no horizontal movement, stop moving
        if (horizontalMovement == 0)
        {
            StopMoving();
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (!canMove) return; // Stop dash input if canMove is false

        if (context.performed && canDash)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    public void DashButton()
    {
        if (canMove && canDash) // Check canMove flag before dashing
        {
            StartCoroutine(DashCoroutine());
        }
    }

    private IEnumerator DashCoroutine()
    {
        canDash = false;
        isDashing = true;

        trailRenderer.emitting = true;

        float dashDirection = isFacingRight ? 1f : -1f;

        rb.velocity = new Vector2(dashDirection * dashSpeed, rb.velocity.y);

        yield return new WaitForSeconds(dashDuration);

        rb.velocity = new Vector2(0f, rb.velocity.y); // Reset horizontal velocity after dash

        isDashing = false;
        trailRenderer.emitting = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (canMove && jumpsRemaining > 0 && context.performed)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            jumpsRemaining--;
            animator.SetTrigger("jump");
        }
    }

    private void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            jumpsRemaining = maxJumps;
            Debug.Log("Grounded - Jumps reset to: " + jumpsRemaining);
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
        if (canMove) horizontalMovement = -1f; // Set to move left
    }

    public void MoveRight()
    {
        if (canMove) horizontalMovement = 1f; // Set to move right
    }

    public void StopMoving()
    {
        horizontalMovement = 0f; // Stop movement
        animator.SetFloat("magnitude", 0f); // Reset animator magnitude
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

    // Method to enable or disable player movement
    public void SetCanMove(bool move)
    {
        canMove = move;
    }
}
