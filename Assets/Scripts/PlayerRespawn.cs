using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Health playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
    }

    public void RespawnToStart()
    {
        // Move player to the starting position of the game/scene
        transform.position = Vector3.zero; // Adjust this to your starting position
        playerHealth.Respawn(); // Call the Respawn method in Health
    }

    public void SetCheckpoint(Transform checkpoint)
    {
        playerHealth.SetCheckpoint(checkpoint); // Set the last checkpoint in Health
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            SetCheckpoint(collision.transform);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
 }
    }
}