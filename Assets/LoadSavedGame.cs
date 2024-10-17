using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSavedGame : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.HasKey("CurrentSceneIndex"))
        {
            int savedSceneIndex = PlayerPrefs.GetInt("CurrentSceneIndex");
            SceneManager.LoadScene(savedSceneIndex);
        }
        else
        {
            Debug.Log("No saved game state found.");
            // Optionally, load the first level or main menu
            // SceneManager.LoadScene(0);
        }
    }
}
