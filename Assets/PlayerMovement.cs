using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    [Header("Movement")]
     public float moveSpeed = 5f;
    float horizontalMovement;

      [Header("Jumping")]
       public float jumpPower = 10f;
       public int maxJumps =2;
       int jumpsRemaining;

      
      [Header("GroundCheck")]
      public Transform groundCheckPos;
      public Vector2 groundCheckSize = new Vector2(0.5f,0.05f);
      public LayerMask groundLayer;


   [Header("Gravity")]
    public float baseGravity=2f;
    public float maxFallSpeed=18f;
    public float fallSpeedMultiplier =2f;

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2 (horizontalMovement * moveSpeed, rb.velocity.y);
        GroundCheck();
        Gravity();
    }


    private void Gravity()
    {
        if(rb.velocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.velocity = new Vector2(rb.velocity.x,Mathf.Max(rb.velocity.y,-maxFallSpeed));

        }
        else    
        {
            rb.gravityScale= baseGravity;

        }

    }
    public void Move (InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }
    public void Jump (InputAction.CallbackContext context){
        if (jumpsRemaining > 0){
        if(context.performed){
            rb.velocity = new Vector2(rb.velocity.x,jumpPower); 
            jumpsRemaining--;
        }else if(context.canceled){
            rb.velocity =  new Vector2(rb.velocity.x,rb.velocity.y * 0.5f);
             jumpsRemaining--;
        }
    }
    }
    private void GroundCheck(){
        if(Physics2D.OverlapBox(groundCheckPos.position,groundCheckSize,0,groundLayer)){
            jumpsRemaining=maxJumps;
        }
        
    }       

    private void OnDrawGizmosSelected(){
        
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position,groundCheckSize);
    }
}
