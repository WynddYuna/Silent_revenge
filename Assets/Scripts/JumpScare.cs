using System.Collections;
using UnityEngine;

public class JumpScare : MonoBehaviour
{
    public GameObject JumpScareImg; // The image to show during the jump scare
    public AudioSource audioSource; // The audio source for the jump scare sound

    // Start is called before the first frame update
    void Start()
    {
        JumpScareImg.SetActive(false); // Ensure the jump scare image is initially hidden
    }

    // Called when another 2D collider enters the trigger collider attached to this object
    private void OnTriggerEnter2D(Collider2D other) // Note the change to Collider2D
    {
        if (other.CompareTag("Player")) // Check if the colliding object is tagged as "Player"
        {
            JumpScareImg.SetActive(true); // Show the jump scare image
            audioSource.Play(); // Play the jump scare sound
            StartCoroutine(DisableImg()); // Start the coroutine to disable the image after a delay
        }
    }

    // Coroutine to disable the jump scare image after a specified time
    IEnumerator DisableImg()
    {
        yield return new WaitForSeconds(2); // Wait for 2 seconds
        JumpScareImg.SetActive(false); // Hide the jump scare image
    }
}