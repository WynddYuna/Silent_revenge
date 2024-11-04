using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(2);

    PlayerPrefs.SetInt("UnlockedLevel", 1); // Reset to initial value
    PlayerPrefs.SetInt("ReachedIndex", 0); // Optionally reset the reached index
    PlayerPrefs.Save(); // Save the changes
    Debug.Log("UnlockedLevel and ReachedIndex have been reset.");
        
    }

    public void ContinueGame()
    {
        // Check if a saved scene index exists
        if (PlayerPrefs.HasKey("CurrentSceneIndex"))
        {
            int currentSceneIndex = PlayerPrefs.GetInt("CurrentSceneIndex");
            SceneManager.LoadScene(currentSceneIndex); // Load the saved scene
        }
        else
        {
            Debug.Log("No saved game found.");
            // Optionally, you can load a default scene or show a message to the player
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
