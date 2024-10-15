using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    private void Start()
    {
        LoadGameState();
    }

    private void LoadGameState()
    {
        // Load the saved game state
        int currentSceneIndex = PlayerPrefs.GetInt("CurrentSceneIndex", 0);

        // Continue the game from the saved scene
        SceneManager.LoadScene(currentSceneIndex);
    }
}