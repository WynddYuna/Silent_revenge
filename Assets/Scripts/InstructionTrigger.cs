using UnityEngine;
using System.Collections;

public class InstructionTrigger : MonoBehaviour
{
    // Public field to assign the instruction text in the Inspector
    public GameObject instructionText; 
    public float displayDuration = 5f; // Duration to display the instruction (set to 5 seconds for testing)

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player")) 
        {
            Debug.Log("Player entered the trigger!"); // Debug message
            StartCoroutine(DisplayInstruction());
        }
    }

    private IEnumerator DisplayInstruction()
    {
        Debug.Log("Displaying instruction text."); // Debug message
        instructionText.SetActive(true); // Show the instruction
        yield return new WaitForSeconds(displayDuration); // Wait for the duration
        instructionText.SetActive(false); // Hide the instruction
        Debug.Log("Hiding instruction text."); // Debug message
    }
}