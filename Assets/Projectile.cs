using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10; // Damage dealt to the boss
    public float speed = 10f; // Speed of the projectile
    public GameObject explosion; // Explosion effect prefab
    public GameObject explosionTwo; // Second explosion effect prefab

    private void Update()
    {
        // Move the projectile forward
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Projectile collided with: " + other.name); // Log what the projectile collides with

        // Check if the projectile hits the boss
        if (other.CompareTag("Boss")) 
        {
            Debug.Log("Hit the Boss!"); // Confirm it's hitting the boss
            
            // Call TakeDamage on the boss
            Boss boss = other.GetComponent<Boss>();
            if (boss != null)
            {
                boss.TakeDamage(damage); // Use the TakeDamage method
            }

            // Instantiate explosion effects
            Instantiate(explosion, transform.position, Quaternion.identity);
            Instantiate(explosionTwo, transform.position, Quaternion.identity);

            // Destroy the projectile
            Destroy(gameObject);
        }
        else if (other.CompareTag("Ground")) // Optional: Destroy projectile on hitting the ground
        {
            Destroy(gameObject);
        }
    }
}