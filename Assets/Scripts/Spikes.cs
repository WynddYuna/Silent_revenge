using UnityEngine;

public class Spikes : MonoBehaviour
{
 [SerializeField] private float damageAmount = 1f; // Amount of damage to deal

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount); // Deal damage to the player
            }
        }
    }
}