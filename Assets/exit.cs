using UnityEngine;
using UnityEditor;

public class ExitGame : MonoBehaviour
{
    public void Exit()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}