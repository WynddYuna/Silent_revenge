using UnityEngine;
using UnityEngine.UI;

public class GamePauseManager : MonoBehaviour
{
    public Button pauseButton; // Reference to the UI button
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
            // Optionally, show a pause menu here
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
            // Optionally, hide the pause menu here
        }
    }
}