using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Required for scene management

public class GameController : MonoBehaviour
{
    int progressAmount;
    public Slider progressSlider;

    void Start()
    {
        progressAmount = 0;
        progressSlider.value = 0;
        Notes.OnNotesCollect += IncreaseProgressAmount;
        HoldToLoadLevel.OnHoldComplete += LoadNextLevel;
    }

    void IncreaseProgressAmount(int amount)
    {
        progressAmount += amount;
        progressSlider.value = progressAmount;

        if (progressAmount >= 100)
        {
            Debug.Log("Level Complete");
            // Optionally trigger the hold to load next level here
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