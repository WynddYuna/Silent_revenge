using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class RegisterController : MonoBehaviour
{
    public string registerUrl = "http://localhost/game_api/register.php"; // URL to your PHP script for registration
    public string loginUrl = "http://localhost/game_api/login.php"; // URL to your PHP script for login
    public TMP_InputField UsernameInput; // Reference to the username input field
    public TMP_InputField PasswordInput; // Reference to the password input field
    public Button registerButton; // Reference to the register button
    public Button loginButton; // Reference to the login button

    void Start()
    {
        // Add listener to the register button
        registerButton.onClick.AddListener(Register);
        // Add listener to the login button
        loginButton.onClick.AddListener(Login);
    }

    void Register()
    {
        // Create a form to send data
        WWWForm form = new WWWForm();
        form.AddField("username", UsernameInput.text);
        form.AddField("password", PasswordInput.text);

        // Start the coroutine to send the request
        StartCoroutine(SendRegistrationRequest(form));
    }

    IEnumerator SendRegistrationRequest(WWWForm form)
    {
        // Send the POST request to the PHP script
        using (UnityWebRequest www = UnityWebRequest.Post(registerUrl, form))
        {
            yield return www.SendWebRequest(); // Wait for the request to complete

            // Check for errors
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Registration Error: " + www.error);
            }
            else
            {
                Debug.Log("Registration Response: " + www.downloadHandler.text); // Log the response from the server
            }
        }
    }

    void Login()
    {
        // Create a form to send data
        WWWForm form = new WWWForm();
        form.AddField("username", UsernameInput.text);
        form.AddField("password", PasswordInput.text);

        // Start the coroutine to send the request
        StartCoroutine(SendLoginRequest(form));
    }

    IEnumerator SendLoginRequest(WWWForm form)
    {
        // Send the POST request to the PHP script
        using (UnityWebRequest www = UnityWebRequest.Post(loginUrl, form))
        {
            yield return www.SendWebRequest(); // Wait for the request to complete

            // Check for errors
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Login Error: " + www.error);
            }
            else
            {
                Debug.Log("Login Response: " + www.downloadHandler.text); // Log the response from the server
                // Here you can handle successful login, like transitioning to another scene or updating UI
            }
        }
    }
}