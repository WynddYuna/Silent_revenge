using UnityEngine;

public class LockTrigger : MonoBehaviour
{
    public GameObject lockpickPanel; // Reference to the lockpicking puzzle UI

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player enters the trigger
        if (other.CompareTag("Player"))
        {
            lockpickPanel.SetActive(true); // Activate the lockpicking puzzle
        }
    }
}
