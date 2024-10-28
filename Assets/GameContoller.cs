using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private int progressAmount;
    public Slider progressSlider;
    public GameObject loadCanvas; // Optional: For UI purposes
    private bool canLoadNextLevel = false; // New variable to track if the level can be loaded
    public Button interactButton; // Reference to the UI button

    void Start()
    {
        progressAmount = 0;
        progressSlider.value = 0;
        Notes.OnNotesCollect += IncreaseProgressAmount;
        interactButton.onClick.AddListener(TryLoadNextLevel); // Add listener to button
        interactButton.gameObject.SetActive(false); // Hide the button initially
    }

    void Update()
    {
        // Show the button if the level is complete
        if (canLoadNextLevel)
        {
            interactButton.gameObject.SetActive(true);
        }
    }

    void IncreaseProgressAmount(int amount)
    {
        progressAmount += amount;
        progressSlider.value = progressAmount;

        if (progressAmount >= 100)
        {
            Debug.Log("Level Complete");
            canLoadNextLevel = true; // Allow loading next level
            loadCanvas.SetActive(true); // Optional: Show loading UI
        }
    }

    void TryLoadNextLevel()
    {
        if (canLoadNextLevel) // Check if the player can load the next level
        {
            LoadNextLevel();
        }
        else
        {
            Debug.Log("You need to collect more items!");
        }
    }

    void LoadNextLevel()
    {
        // Load the next level based on the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

        // Load the next scene
        SceneManager.LoadScene(nextSceneIndex);
    }
}