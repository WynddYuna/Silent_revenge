using UnityEngine;

public class OpenUserManagementLink : MonoBehaviour // Corrected class name
{
    // URL of the user management page
    private string userManagementUrl = "http://192.168.0.141/game_login/login.html"; // Change this to your actual URL

    // Method to open the URL
    public void OpenLink()
    {
        Application.OpenURL(userManagementUrl);
    }
}