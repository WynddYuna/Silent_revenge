using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth = 3; // Maximum health points
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    private Transform lastCheckpoint;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;

        // Only reduce health if the player is not already dead
        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());

            // Respawn at the last checkpoint after taking damage
            RespawnAtCheckpoint();
        }

        // Check if health is zero after taking damage
        if (currentHealth <= 0)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        if (!dead)
        {
            dead = true;

            // Deactivate all attached component classes
            foreach (Behaviour component in components)
                component.enabled = false;

            anim.SetBool("grounded", true);
            anim.SetTrigger("die");

            // Respawn at the start when dead
            FindObjectOfType<PlayerRespawn>().RespawnToStart(); // Respawn at the beginning
        }
    }

    public void Respawn()
    {
        // Reset health if the player is dead
        if (currentHealth <= 0)
        {
            currentHealth = startingHealth; // Restore health to starting value
        }

        anim.ResetTrigger("die");
        anim.Play("idle");

        // Activate all attached component classes
        foreach (Behaviour component in components)
            component.enabled = true;

        dead = false; // Reset dead state
    }

    public void SetCheckpoint(Transform checkpoint)
    {
        lastCheckpoint = checkpoint; // Set the last checkpoint
    }

    public void RespawnAtCheckpoint()
    {
        if (lastCheckpoint != null)
        {
            transform.position = lastCheckpoint.position; // Move player to checkpoint location
            
            // Reduce health only after confirming respawn
            if (currentHealth > 0)
            {
                currentHealth--; // Reduce health on respawn
            }

            // Check if health is zero after respawn
            if (currentHealth <= 0)
            {
                HandleDeath(); // Handle death if health is zero
            }
            else
            {
                Respawn(); // Call the Respawn method in Health
            }
        }
    }

    private IEnumerator Invunerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }
}