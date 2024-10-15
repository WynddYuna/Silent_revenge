using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveAndQuitButton : MonoBehaviour
{
    public void SaveAndQuit()
    {
        // Save the game state
        SaveGameState();

        // Quit the game
        Application.Quit();
    }

    private void SaveGameState()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Save the current scene index to a file or a database
        // For example, you can use PlayerPrefs to save the scene index
        PlayerPrefs.SetInt("CurrentSceneIndex", currentSceneIndex);

        // You can also save other game state data, such as player progress, scores, etc.
    }
}