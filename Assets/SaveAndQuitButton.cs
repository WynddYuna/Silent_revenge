using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveAndQuitButton : MonoBehaviour
{
    public void SaveAndQuit()
    {
        // Save the game state
        SaveGameState();
        // Quit the game
        //Application.Quit();
        SceneManager.LoadSceneAsync(0);

#if UNITY_EDITOR
        // Exit play mode in the editor
        //UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void SaveGameState()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // Save the current scene index to PlayerPrefs
        PlayerPrefs.SetInt("CurrentSceneIndex", currentSceneIndex);
        PlayerPrefs.Save();
    }
}
