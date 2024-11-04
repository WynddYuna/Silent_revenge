
using UnityEngine;
using UnityEngine.UI;

public class GamePauseManager : MonoBehaviour
{
    public Button pauseButton; // Reference to the UI button
    public PlayerMovement playerMovement; // Reference to the PlayerMovement script
    private bool isPaused = false; // Track the pause state

    void Start()
    {
        // Add listener to the button
        pauseButton.onClick.AddListener(TogglePause);
    }

    void Update()
    {
        // Optional: Allow pausing with the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused; // Toggle the pause state

        if (isPaused)
        {
            Time.timeScale = 0f; // Pause the game
            playerMovement.SetCanMove(false); // Disable player movement
            // Optionally, show a pause menu here
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
            playerMovement.SetCanMove(true); // Enable player movement
            // Optionally, hide the pause menu here
        }
    }
}
