using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Add this line to include scene management

public class Boss : MonoBehaviour
{
    public int health = 100; // Boss health
    public int damage = 10; // Damage dealt to the player
    private float timeBtwDamage = 1.5f; // Time between damage to the player
    public Animator camAnim; // Reference to the camera animator
    public Slider healthBar; // UI health bar reference
    private Animator anim; // Animator for the boss
    public bool isDead = false; // Check if the boss is dead

    public PlayerMovement playerMovement;

    private void Start()
    {
        anim = GetComponent<Animator>();
        healthBar.value = health; // Initialize health bar
    }

    private void Update()
    {
        // Change boss animation based on health
        if (health <= 25 && !isDead) 
        {
            anim.SetTrigger("stageTwo");
        }

        // Check if the boss is dead
        if (health <= 0 && !isDead) 
        {
            isDead = true;
            anim.SetTrigger("death");
            Debug.Log("Boss is dead!"); // Debug log
            Die(); // Call die method
        }

        // Handle timing for player damage
        if (timeBtwDamage > 0) 
        {
            timeBtwDamage -= Time.deltaTime;
        }

        // Update health bar
        healthBar.value = health;
    }

    // Method to take damage
    public void TakeDamage(int damage)
    {
        if (isDead) return; // Prevent taking damage if already dead

        health -= damage; // Reduce health
        healthBar.value = health; // Update health bar
        Debug.Log($"Boss took damage: {damage}. Current health: {health}"); // Debug log

        if (health <= 0)
        {
            isDead = true; // Set to dead
            anim.SetTrigger("death"); // Trigger death animation
            Debug.Log("Boss is dying!"); // Debug log
            Die(); // Call die method
        }
    }

    private void Die()
    {
        // Handle boss death (e.g., play animation, drop loot)
        Debug.Log("Destroying boss game object."); // Debug log
        
        // Start a coroutine to wait for the animation to finish before loading the main menu
        StartCoroutine(DieAndLoadMenuCoroutine());
    }

    private IEnumerator DieAndLoadMenuCoroutine()
    {
        // Wait for a few seconds to allow the death animation to play
        yield return new WaitForSeconds(2f); // Adjust time as needed
        Destroy(gameObject); // Destroy boss game object

        // Load the main menu scene
        SceneManager.LoadScene("Main Menu"); // Use the exact name of your main menu scene
    }

    // Change access modifier to public
    public void OnTriggerEnter2D(Collider2D other)
    {

        



        
        // Deal damage to the player
        if (other.CompareTag("Player") && !isDead) 
        {
            if (timeBtwDamage <= 0) 
            {
               
                other.GetComponent<PlayerMovement>().health -= damage; // Damage player
                timeBtwDamage = 1.5f; // Reset damage timing
            }
        } 
    }

    private void OnCollisionEnter2D (Collision2D collision){



        if(collision.gameObject.tag=="Player")
        {

             playerMovement.KBCounter =playerMovement.KBTotalTime;
             if(collision.transform.position.x <= transform.position.x)
             {

             }

             playerMovement.KnockFromRight=true;

        }
             playerMovement.KBCounter =playerMovement.KBTotalTime;
             if(collision.transform.position.x > transform.position.x)
             {

             }

             playerMovement.KnockFromRight=false;

        
    }
}