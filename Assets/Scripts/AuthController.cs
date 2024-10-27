using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement; // Required for scene management

public class AuthController : MonoBehaviour
{
    public string registerUrl = "http://localhost/game_api/register.php"; // URL to your PHP script for registration
    public string loginUrl = "http://localhost/game_api/login.php"; // URL to your PHP script for login
    public string deleteUrl = "http://localhost/game_api/delete.php"; // URL to your PHP script for account deletion

    public TMP_InputField UsernameInput; // Reference to the username input field
    public TMP_InputField PasswordInput; // Reference to the password input field
    public Button registerButton; // Reference to the register button
    public Button loginButton; // Reference to the login button
    public Button deleteButton; // Reference to the delete button
    public TMP_Text feedbackText; // Reference to a TextMeshProUGUI element for feedback

    private const int MinLength = 6;
    private const int MaxLength = 16;

    void Start()
    {
        // Add listener to the register button
        registerButton.onClick.AddListener(Register);
        // Add listener to the login button
        loginButton.onClick.AddListener(Login);
        // Add listener to the delete button
        deleteButton.onClick.AddListener(DeleteAccount);
    }

    void Register()
    {
        if (IsInputValid(UsernameInput.text, PasswordInput.text))
        {
            // Create a form to send data
            WWWForm form = new WWWForm();
            form.AddField("username", UsernameInput.text);
            form.AddField("password", PasswordInput.text);

            // Start the coroutine to send the request
            StartCoroutine(SendRegistrationRequest(form));
        }
    }

    IEnumerator SendRegistrationRequest(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(registerUrl, form))
        {
            yield return www.SendWebRequest(); // Wait for the request to complete

            // Check for errors
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                feedbackText.text = "Registration Error: " + www.error;
            }
            else
            {
                feedbackText.text = "Registration Response: " + www.downloadHandler.text; // Log the response from the server
                ClearInputs(); // Clear the input fields after registration
            }
        }
    }

    void Login()
    {
        if (IsInputValid(UsernameInput.text, PasswordInput.text))
        {
            // Create a form to send data
            WWWForm form = new WWWForm();
            form.AddField("username", UsernameInput.text);
            form.AddField("password", PasswordInput.text);

            // Start the coroutine to send the request
            StartCoroutine(SendLoginRequest(form));
        }
    }

    IEnumerator SendLoginRequest(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(loginUrl, form))
        {
            yield return www.SendWebRequest(); // Wait for the request to complete

            // Check for errors
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                feedbackText.text = "Login Error: " + www.error;
            }
            else
            {
                feedbackText.text = "Login Response: " + www.downloadHandler.text; // Log the response from the server
                
                // Check for successful login
                if (www.downloadHandler.text == "Login successful") // Adjust this based on your PHP response
                {
                    // Load the main scene after successful login
                    SceneManager.LoadScene("MainScene"); // Replace with your actual scene name
                }
                else
                {
                    // Display the error message returned from the server
                    feedbackText.text = www.downloadHandler.text;
                }
                ClearInputs(); // Clear the input fields after login
            }
        }
    }

    void DeleteAccount()
    {
        if (IsInputValid(UsernameInput.text, PasswordInput.text))
        {
            // Create a form to send data
            WWWForm form = new WWWForm();
            form.AddField("username", UsernameInput.text);
            form.AddField("password", PasswordInput.text); // Optional: include password for verification

            // Start the coroutine to send the request
            StartCoroutine(SendDeleteRequest(form));
        }
    }

    IEnumerator SendDeleteRequest(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(deleteUrl, form))
        {
            yield return www.SendWebRequest(); // Wait for the request to complete

            // Check for errors
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                feedbackText.text = "Delete Account Error: " + www.error;
            }
            else
            {
                                feedbackText.text = "Delete Account Response: " + www.downloadHandler.text; // Log the response from the server
                ClearInputs(); // Clear the input fields after deletion
            }
        }
    }

    private bool IsInputValid(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            feedbackText.text = "Username and password cannot be empty.";
            return false;
        }

        if (username.Length < MinLength || username.Length > MaxLength)
        {
            feedbackText.text = $"Username must be between {MinLength} and {MaxLength} characters.";
            return false;
        }

        return true; // Input is valid
    }

    private void ClearInputs()
    {
        UsernameInput.text = string.Empty; // Clear the username input field
        PasswordInput.text = string.Empty; // Clear the password input field
    }
}