using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int health = 100; // Boss health
    public int damage; // Damage dealt to the player
    private float timeBtwDamage = 1.5f; // Time between damage to the player
    public Animator camAnim; // Reference to the camera animator
    public Slider healthBar; // UI health bar reference
    private Animator anim; // Animator for the boss
    public bool isDead = false; // Check if the boss is dead

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
        Destroy(gameObject); // Destroy boss game object
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Deal damage to the player
        if (other.CompareTag("Player") && !isDead) 
        {
            if (timeBtwDamage <= 0) 
            {
                camAnim.SetTrigger("shake");
                other.GetComponent<PlayerMovement>().health -= damage; // Damage player
                timeBtwDamage = 1.5f; // Reset damage timing
            }
        } 
    }
}