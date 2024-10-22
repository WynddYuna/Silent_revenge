using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RegisterController : MonoBehaviour
{
    public string apiUrl = "http://localhost/game_api/register.php"; // URL to your PHP script
    public InputField UsernameInput; // Reference to the username input field
    public InputField PasswordInput; // Reference to the password input field
    public Button registerButton; // Reference to the register button

    void Start()
    {
        // Add listener to the register button
        registerButton.onClick.AddListener(Register);
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
        using (UnityWebRequest www = UnityWebRequest.Post(apiUrl, form))
        {
            yield return www.SendWebRequest(); // Wait for the request to complete

            // Check for errors
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                Debug.Log("Response: " + www.downloadHandler.text); // Log the response from the server
            }
        }
    }
}