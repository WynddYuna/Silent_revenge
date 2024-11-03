using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

// Class to represent the server response structure
[System.Serializable]
public class ServerResponse
{
    public string success; // Field to hold success message
    public string error;   // Field to hold error message
}

public class AuthController : MonoBehaviour
{
    // Change these URLs to match your server's IP address and endpoint
    public string registerUrl = "http://192.168.203.243/game_api/register.php"; 
    public string loginUrl = "http://192.168.203.243/game_api/login.php"; 
    public string deleteUrl = "http:/192.168.203.243/game_api/delete.php"; 

    public TMP_InputField UsernameInput; 
    public TMP_InputField PasswordInput; 
    public Button registerButton; 
    public Button loginButton; 
    public Button deleteButton; 
    public TMP_Text feedbackText; 

    private const int MinLength = 6;
    private const int MaxLength = 16;

    void Start()
    {
        registerButton.onClick.AddListener(Register);
        loginButton.onClick.AddListener(Login);
        deleteButton.onClick.AddListener(DeleteAccount);
    }

    void Register()
    {
        if (IsInputValid(UsernameInput.text, PasswordInput.text))
        {
            WWWForm form = new WWWForm();
            form.AddField("username", UsernameInput.text);
            form.AddField("password", PasswordInput.text);
            StartCoroutine(SendRegistrationRequest(form));
            Debug.Log("Username: " + UsernameInput.text);
            Debug.Log("Password: " + PasswordInput.text);
        }
    }

    IEnumerator SendRegistrationRequest(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(registerUrl, form))
        {
            // Log the request URL and method
            Debug.Log("Sending request to: " + registerUrl);
            Debug.Log("Request method: POST");
            yield return www.SendWebRequest();


            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                feedbackText.text = "Registration Error: " + www.error;
            }
            else
            {
                feedbackText.text = "Registration Response: " + www.downloadHandler.text;
                ClearInputs();
            }
        }
    }

    void Login()
    {
        if (IsInputValid(UsernameInput.text, PasswordInput.text))
        {
            WWWForm form = new WWWForm();
            form.AddField("username", UsernameInput.text);
            form.AddField("password", PasswordInput.text);
            StartCoroutine(SendLoginRequest(form));
        }
    }

    IEnumerator SendLoginRequest(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(loginUrl, form))
        {
            Debug.Log("Sending request to: " + loginUrl);
            Debug.Log("Request method: POST");
            Debug.Log("Form data: " + form.ToString());

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                feedbackText.text = "Login Error: " + www.error;
            }
            else
            {
                // Parse JSON response
                var jsonResponse = JsonUtility.FromJson<ServerResponse>(www.downloadHandler.text);
                feedbackText.text = jsonResponse.success ?? jsonResponse.error;

                // Check for successful login
                   if (jsonResponse.success != null)
                {
                    // Load the new scene here
                    SceneManager.LoadScene(1); // Replace with your actual scene name
                }
                ClearInputs();
            }
        }
    }

    void DeleteAccount()
    {
        if (IsInputValid(UsernameInput.text, PasswordInput.text))
        {
            WWWForm form = new WWWForm();
            form.AddField("username", UsernameInput.text);
            form.AddField("password", PasswordInput.text); // Optional: include password for verification
            StartCoroutine(SendDeleteRequest(form));
        }
    }

    IEnumerator SendDeleteRequest(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(deleteUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                feedbackText.text = "Delete Account Error: " + www.error;
            }
            else
            {
                feedbackText.text = "Delete Account Response: " + www.downloadHandler.text;
                ClearInputs();
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

        // Input is valid
        return true; 
    }

    private void ClearInputs()
    {
        UsernameInput.text = string.Empty; // Clear the username input field
        PasswordInput.text = string.Empty; // Clear the password input field
    }
}
