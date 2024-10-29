using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
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
